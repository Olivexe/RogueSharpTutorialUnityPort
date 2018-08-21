using System.Text;
using RogueSharp;
using RogueSharp.DiceNotation;
using RogueSharpTutorial.Model;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Controller
{
    public class CommandSystem
    {
        private Game game;
        public  bool IsPlayerTurn   { get; set; }

        public CommandSystem(Game game)
        {
            this.game = game;
        }

        /// <summary>
        /// Return value is true if the player was able to move false when the player couldn't move, such as trying to move into a wall.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool MovePlayer(Direction direction)
        {
            int x = game.Player.X;
            int y = game.Player.Y;

            switch (direction)
            {
                case Direction.Up:
                    {
                        y = game.Player.Y + 1;
                        break;
                    }
                case Direction.Down:
                    {
                        y = game.Player.Y - 1;
                        break;
                    }
                case Direction.Left:
                    {
                        x = game.Player.X - 1;
                        break;
                    }
                case Direction.Right:
                    {
                        x = game.Player.X + 1;
                        break;
                    }
                case Direction.UpLeft:
                    {
                        x = game.Player.X - 1;
                        y = game.Player.Y + 1;
                        break;
                    }
                case Direction.UpRight:
                    {
                        x = game.Player.X + 1;
                        y = game.Player.Y + 1;
                        break;
                    }
                case Direction.DownLeft:
                    {
                        x = game.Player.X - 1;
                        y = game.Player.Y - 1;
                        break;
                    }
                case Direction.DownRight:
                    {
                        x = game.Player.X + 1;
                        y = game.Player.Y - 1;
                        break;
                    }
                default:
                    {
                        return false;
                    }
            }

            if (game.World.SetActorPosition(game.Player, x, y))
            {
                return true;
            }

            Monster monster = game.World.GetMonsterAt(x, y);

            if (monster != null)
            {
                Attack(game.Player, monster);
                return true;
            }

            return false;
        }

        public void EndPlayerTurn()
        {
            IsPlayerTurn = false;
        }

        public void ActivateMonsters()
        {
            IScheduleable scheduleable = game.SchedulingSystem.Get();

            if (scheduleable is Player)
            {
                IsPlayerTurn = true;
                game.SchedulingSystem.Add(game.Player);
            }
            else
            {
                if (game.PlayerDied)                                                 // If player was killed by a monster, stop moving monsters.
                {
                    return;
                }

                Monster monster = scheduleable as Monster;

                if (monster != null)
                {
                    monster.PerformAction(this);
                    game.SchedulingSystem.Add(monster);
                }

                ActivateMonsters();
            }
        }

        public void MoveMonster(Monster monster, Cell cell)
        {
            if (!game.World.SetActorPosition(monster, cell.X, cell.Y))
            {
                if (game.Player.X == cell.X && game.Player.Y == cell.Y)
                {
                    Attack(monster, game.Player);
                }
            }
        }

        public void Attack(Actor attacker, Actor defender)
        {
            int hits = ResolveAttack(attacker, defender);

            int blocks = ResolveDefense(defender, hits);

            ResolveDamage(attacker, defender, hits, blocks);
        }

        /// <summary>
        /// The attacker rolls based on his stats to see if he gets any hits.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        /// <param name="attackMessage"></param>
        /// <returns></returns>
        private int ResolveAttack(Actor attacker, Actor defender)
        {
            int hits = 0;

            DiceExpression attackDice = new DiceExpression().Dice(attacker.Attack, 100);        // Roll a number of 100-sided dice equal to the Attack value of the attacking actor
            DiceResult attackResult = attackDice.Roll();

            foreach (TermResult termResult in attackResult.Results)                             // Look at the face value of each single die that was rolled
            {
                if (termResult.Value >= 100 - attacker.AttackChance)                            // Compare the value to 100 minus the attack chance and add a hit if it's greater
                {
                    hits++;
                }
            }

            return hits;
        }

        // The defender rolls based on his stats to see if he blocks any of the hits from the attacker
        private int ResolveDefense(Actor defender, int hits)
        {
            int blocks = 0;

            if (hits > 0)
            {
                DiceExpression defenseDice = new DiceExpression().Dice(defender.Defense, 100);  // Roll a number of 100-sided dice equal to the Defense value of the defendering actor
                DiceResult defenseRoll = defenseDice.Roll();

                foreach (TermResult termResult in defenseRoll.Results)                          // Look at the face value of each single die that was rolled
                {
                    if (termResult.Value >= 100 - defender.DefenseChance)                       // Compare the value to 100 minus the defense chance and add a block if it's greater
                    {
                        blocks++;
                    }
                }
            }
 
            return blocks;
        }

        // Apply any damage that wasn't blocked to the defender
        private void ResolveDamage(Actor attacker, Actor defender, int hits, int blocks)
        {
            StringBuilder attackMessage = new StringBuilder();

            if (attacker is Player)
            {
                attackMessage.AppendFormat("You attack the {1}", attacker.Name, defender.Name);
            }
            else if (defender is Player)
            {
                attackMessage.AppendFormat("The {0} attacks you", attacker.Name, defender.Name);
            }
            else
            {
                attackMessage.AppendFormat("The {0} attacks {1} ", attacker.Name, defender.Name);
            }

            int damage = hits - blocks;

            if (hits < 1)
            {
                if(attacker is Player)
                {
                    attackMessage.Append(" and miss completely.");
                }
                else if (defender is Player)
                {
                    attackMessage.Append(" and misses completely.");
                }
                else
                {
                    attackMessage.Append(" and misses completely.");
                }
            }
            else if (damage > 0)
            {
                defender.Health = defender.Health - damage;

                attackMessage.Append(" for " + damage + " damage.");
            }
            else
            {
                if (attacker is Player)
                {
                    attackMessage.AppendFormat(" and the {0} blocked all damage.", defender.Name);
                }
                else if (defender is Player)
                {
                    attackMessage.AppendFormat(" and you blocked all damage.");
                }
                else
                {
                    attackMessage.AppendFormat(" and the {0} blocked all damage.", defender.Name);
                }
            }

            game.MessageLog.Add(attackMessage.ToString());

            if (defender.Health <= 0)
            {
                ResolveDeath(attacker, defender);
            }
        }

        // Remove the defender from the map and add some messages upon death.
        private void ResolveDeath(Actor attacker, Actor defender)
        {
            if (defender is Player)
            {
                game.MessageLog.Add("The " + attacker.Name + " killed you, GAME OVER MAN!");

                game.ResolvePlayerDeath();
            }
            else if (defender is Monster)
            {
                if (defender.Head != null && defender.Head != HeadEquipment.None(game))
                {
                    game.World.AddTreasure(defender.X, defender.Y, defender.Head);
                }
                if (defender.Body != null && defender.Body != BodyEquipment.None(game))
                {
                    game.World.AddTreasure(defender.X, defender.Y, defender.Body);
                }
                if (defender.Hand != null && defender.Hand != HandEquipment.None(game))
                {
                    game.World.AddTreasure(defender.X, defender.Y, defender.Hand);
                }
                if (defender.Feet != null && defender.Feet != FeetEquipment.None(game))
                {
                    game.World.AddTreasure(defender.X, defender.Y, defender.Feet);
                }

                game.Player.Score++;

                game.World.AddGold(defender.X, defender.Y, defender.Gold);
                game.World.RemoveMonster((Monster)defender);

                game.MessageLog.Add($"The {defender.Name} died and dropped {defender.Gold} gold");
            }
        }
        
        public static bool UseItem(int itemNum, Game game)
        { 
            bool didUseItem = false;

            if (itemNum == 1)
            {
                didUseItem = game.Player.Item1.Use();
            }
            else if (itemNum == 2)
            {
                didUseItem = game.Player.Item2.Use();
            }
            else if (itemNum == 3)
            {
                didUseItem = game.Player.Item3.Use();
            }
            else if (itemNum == 4)
            {
                didUseItem = game.Player.Item4.Use();
            }

            if (didUseItem )
            {
                RemoveItemsWithNoRemainingUses(game);
            }

            return didUseItem;
      }
        
        private static void RemoveItemsWithNoRemainingUses(Game game)
        {
            if (game.Player.Item1.RemainingUses <= 0)
            {
                game.Player.Item1 = new NoItem(game);
            }
            if (game.Player.Item2.RemainingUses <= 0)
            {
                game.Player.Item2 = new NoItem(game);
            }
            if (game.Player.Item3.RemainingUses <= 0)
            {
                game.Player.Item3 = new NoItem(game);
            }
            if (game.Player.Item4.RemainingUses <= 0)
            {
                game.Player.Item4 = new NoItem(game);
            }
        }
    }
}