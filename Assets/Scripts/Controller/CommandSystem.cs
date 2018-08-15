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
            StringBuilder attackMessage = new StringBuilder();
            StringBuilder defenseMessage = new StringBuilder();

            int hits = ResolveAttack(attacker, defender, attackMessage);

            int blocks = ResolveDefense(defender, hits, attackMessage, defenseMessage);

            game.MessageLog.Add(attackMessage.ToString());
            if (!string.IsNullOrEmpty(defenseMessage.ToString()))
            {
                game.MessageLog.Add(defenseMessage.ToString());
            }

            int damage = hits - blocks;

            ResolveDamage(defender, damage);
        }

        /// <summary>
        /// The attacker rolls based on his stats to see if he gets any hits.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        /// <param name="attackMessage"></param>
        /// <returns></returns>
        private int ResolveAttack(Actor attacker, Actor defender, StringBuilder attackMessage)
        {
            int hits = 0;

            attackMessage.AppendFormat("{0} attacks {1} and rolls: ", attacker.Name, defender.Name);

            // Roll a number of 100-sided dice equal to the Attack value of the attacking actor
            DiceExpression attackDice = new DiceExpression().Dice(attacker.Attack, 100);
            DiceResult attackResult = attackDice.Roll();

            // Look at the face value of each single die that was rolled
            foreach (TermResult termResult in attackResult.Results)
            {
                attackMessage.Append(termResult.Value + ", ");
                // Compare the value to 100 minus the attack chance and add a hit if it's greater
                if (termResult.Value >= 100 - attacker.AttackChance)
                {
                    hits++;
                }
            }

            return hits;
        }

        // The defender rolls based on his stats to see if he blocks any of the hits from the attacker
        private int ResolveDefense(Actor defender, int hits, StringBuilder attackMessage, StringBuilder defenseMessage)
        {
            int blocks = 0;

            if (hits > 0)
            {
                attackMessage.AppendFormat("scoring {0} hits.", hits);
                defenseMessage.AppendFormat("  {0} defends and rolls: ", defender.Name);

                // Roll a number of 100-sided dice equal to the Defense value of the defendering actor
                DiceExpression defenseDice = new DiceExpression().Dice(defender.Defense, 100);
                DiceResult defenseRoll = defenseDice.Roll();

                // Look at the face value of each single die that was rolled
                foreach (TermResult termResult in defenseRoll.Results)
                {
                    defenseMessage.Append(termResult.Value + ", ");
                    // Compare the value to 100 minus the defense chance and add a block if it's greater
                    if (termResult.Value >= 100 - defender.DefenseChance)
                    {
                        blocks++;
                    }
                }
                defenseMessage.AppendFormat("resulting in {0} blocks.", blocks);
            }
            else
            {
                attackMessage.Append("and misses completely.");
            }

            return blocks;
        }

        // Apply any damage that wasn't blocked to the defender
        private void ResolveDamage(Actor defender, int damage)
        {
            if (damage > 0)
            {
                defender.Health = defender.Health - damage;

                game.MessageLog.Add(defender.Name + " was hit for " + damage + " damage.");

                if (defender.Health <= 0)
                {
                    ResolveDeath(defender);
                }
            }
            else
            {
                game.MessageLog.Add(defender.Name + " blocked all damage.");
            }
        }

        // Remove the defender from the map and add some messages upon death.
        private void ResolveDeath(Actor defender)
        {
            if (defender is Player)
            {
                game.MessageLog.Add(defender.Name + " was killed, GAME OVER MAN!");
            }
            else if (defender is Monster)
            {
                game.World.RemoveMonster((Monster)defender);

                game.MessageLog.Add(defender.Name + " died and dropped " + defender.Gold + " gold.");
            }
        }
    }
}