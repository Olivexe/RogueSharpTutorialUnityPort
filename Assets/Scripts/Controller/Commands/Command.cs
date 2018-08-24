using System.Text;
using RogueSharp;
using RogueSharp.DiceNotation;
using RogueSharpTutorial.Model;

namespace RogueSharpTutorial.Controller
{
    public static class Command
    {
        public static Game          Game    { get; private set; }
        public static DungeonMap    World   { get; private set; }

        public static void SetGame(Game game)
        {
            Game  = game;
            World = game.World;
        }

        public static void Move(Actor actor, ICell cell)
        {
            Move(actor, cell.X, cell.Y);
        }

        public static bool Move(Actor actor, int x, int y)
        {
            if (Game == null || World == null) return false;                        // Get out if Game or World have not been initialized

            if (World.SetActorPosition(actor, x, y))
            {
                return true;                                                        // Nothing in the space and is walkable, so move and get out
            }

            if (actor is Player && World.GetMonsterAt(x, y) != null)                // If actor is the Player and check to see if it is a monster preventing moving
            {
                Attack(actor, World.GetMonsterAt(x, y));                            // Attack the monster and get out
                return true;    
            }
            else if (actor is Monster && World.GetPlayerAt(x, y) != null)           // Actor is not a player so check to see if it is a monster
            {
                Attack(actor, World.GetPlayerAt(x, y));
                return true;
            }

            return false;
        }

        public static void Attack(Actor attacker, Actor defender)
        {
            int hits = ResolveAttack(attacker, defender);

            int blocks = ResolveDefense(defender, hits);

            ResolveDamage(attacker, defender, hits, blocks);
        }

        private static int ResolveAttack(Actor attacker, Actor defender)
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

        private static int ResolveDefense(Actor defender, int hits)
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

        private static void ResolveDamage(Actor attacker, Actor defender, int hits, int blocks)
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
                if (attacker is Player)
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

            Game.MessageLog.Add(attackMessage.ToString());

            if (defender.Health <= 0)
            {
                ResolveDeath(attacker, defender);
            }
        }

        private static void ResolveDeath(Actor attacker, Actor defender)
        {
            if (defender is Player)
            {
                Game.MessageLog.Add("The " + attacker.Name + " killed you, GAME OVER MAN!");

                Game.ResolvePlayerDeath();
            }
            else if (defender is Monster)
            {
                if (defender.Head != null && defender.Head != HeadEquipment.None(Game))
                {
                    World.AddTreasure(defender.X, defender.Y, defender.Head);
                }
                if (defender.Body != null && defender.Body != BodyEquipment.None(Game))
                {
                    World.AddTreasure(defender.X, defender.Y, defender.Body);
                }
                if (defender.Hand != null && defender.Hand != HandEquipment.None(Game))
                {
                    World.AddTreasure(defender.X, defender.Y, defender.Hand);
                }
                if (defender.Feet != null && defender.Feet != FeetEquipment.None(Game))
                {
                    World.AddTreasure(defender.X, defender.Y, defender.Feet);
                }

                Game.Player.MonsterScore++;

                World.AddGold(defender.X, defender.Y, defender.Gold);
                World.RemoveMonster((Monster)defender);

                Game.MessageLog.Add($"The {defender.Name} died and dropped {defender.Gold} gold");
            }
        }
    }
}