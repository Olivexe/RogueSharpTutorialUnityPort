using System.Text;
using System.Collections.Generic;
using RogueSharp;
using RogueSharp.DiceNotation;
using RogueSharpTutorial.Model;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Controller
{
    public static class Command
    {
        public static Game Game { get; private set; }
        public static DungeonMap World { get; private set; }

        public static void SetGame(Game game)
        {
            Game = game;
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
                Attack(actor, World.GetMonsterAt(x, y), true);                       // Attack the monster and get out
                return true;
            }
            else if (actor is Monster && World.GetPlayerAt(x, y) != null)           // Actor is not a player so check to see if it is a monster
            {
                Attack(actor, World.GetPlayerAt(x, y), true);
                return true;
            }

            return false;
        }

        public static bool PlayerPickUp(int position)
        {
            if (position < -1 || position > 3)
            {
                return false;                                                           //Should not encounter this but just in case
            }

            if(Game.Player.Inventory.Count >= Game.Player.MaxInventory)
            {
                Game.MessageLog.Add("You have no room to pick up anything.");
                return false;
            }

            List<TreasurePile> treasures = World.GetAllTreasurePilesAt(Game.Player.X, Game.Player.Y);

            if (position > -1)
            {
                treasures[position].Treasure.PickUp(Game.Player);
                World.RemoveTreasure(treasures[position]);
            }
            else
            {
                foreach (TreasurePile treasure in treasures)
                {
                    treasure.Treasure.PickUp(Game.Player);
                    World.RemoveTreasure(treasure);
                }
            }
            return true;
        }

        public static bool Attack(Actor attacker, Actor defender, bool isMelee)
        {
            int hits = ResolveAttack(attacker, defender, isMelee);

            int blocks = ResolveDefense(defender, hits);

            return ResolveDamage(attacker, defender, hits, blocks);
        }

        private static int ResolveAttack(Actor attacker, Actor defender, bool isMelee)
        {
            int hits = 0;

            if (isMelee)
            {
                DiceExpression attackDice = new DiceExpression().Dice(attacker.AttackMeleeAdjusted, 100);// Roll a number of 100-sided dice equal to the Attack value of the attacking actor
                DiceResult attackResult = attackDice.Roll();

                foreach (TermResult termResult in attackResult.Results)                             // Look at the face value of each single die that was rolled
                {
                    if (termResult.Value >= 100 - attacker.AttackChanceMeleeAdjusted)                    // Compare the value to 100 minus the attack chance and add a hit if it's greater
                    {
                        hits++;
                    }
                }
            }
            else
            {
                DiceExpression attackDice = new DiceExpression().Dice(attacker.AttackRangedAdjusted, 100);// Roll a number of 100-sided dice equal to the Attack value of the attacking actor
                DiceResult attackResult = attackDice.Roll();

                foreach (TermResult termResult in attackResult.Results)                             // Look at the face value of each single die that was rolled
                {
                    if (termResult.Value >= 100 - attacker.AttackChanceRangedAdjusted)                    // Compare the value to 100 minus the attack chance and add a hit if it's greater
                    {
                        hits++;
                    }
                }
            }
            return hits;
        }

        private static int ResolveDefense(Actor defender, int hits)
        {
            int blocks = 0;

            if (hits > 0)
            {
                DiceExpression defenseDice = new DiceExpression().Dice(defender.DefenseAdjusted, 100);// Roll a number of 100-sided dice equal to the Defense value of the defendering actor
                DiceResult defenseRoll = defenseDice.Roll();

                foreach (TermResult termResult in defenseRoll.Results)                          // Look at the face value of each single die that was rolled
                {
                    if (termResult.Value >= 100 - defender.DefenseChanceAdjusted)               // Compare the value to 100 minus the defense chance and add a block if it's greater
                    {
                        blocks++;
                    }
                }
            }

            return blocks;
        }

        private static bool ResolveDamage(Actor attacker, Actor defender, int hits, int blocks)
        {
            bool causedDamage = false;

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
                defender.CurrentHealth -= damage;
                attackMessage.Append(" for " + damage + " damage.");
                causedDamage = true;
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

            if (defender.CurrentHealth <= 0)
            {
                ResolveDeath(attacker, defender);
                causedDamage = false;
            }

            return causedDamage;
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
                if (defender.Hands != null && defender.Hands != HandsEquipment.None(Game))
                {
                    World.AddTreasure(defender.X, defender.Y, defender.Hands);
                }
                if (defender.Feet != null && defender.Feet != FeetEquipment.None(Game))
                {
                    World.AddTreasure(defender.X, defender.Y, defender.Feet);
                }
                if (defender.MainHand != null && defender.MainHand != MainHandEquipment.None(Game))
                {
                    World.AddTreasure(defender.X, defender.Y, defender.MainHand);
                }
                if (defender.Ranged != null && defender.Ranged != RangedEquipment.None(Game))
                {
                    World.AddTreasure(defender.X, defender.Y, defender.Ranged);
                }
                if (defender.AmmoCarried != null && defender.AmmoCarried != Ammunition.None(Game))
                {
                    World.AddTreasure(defender.X, defender.Y, defender.AmmoCarried);
                }

                if (defender.Item1 != null && !(defender.Item1 is NoItem))
                {
                    World.AddTreasure(defender.X, defender.Y, defender.Item1 as ITreasure);
                }
                if (defender.Item2 != null && !(defender.Item2 is NoItem))
                {
                    World.AddTreasure(defender.X, defender.Y, defender.Item2 as ITreasure);
                }
                if (defender.Item3 != null && !(defender.Item3 is NoItem))
                {
                    World.AddTreasure(defender.X, defender.Y, defender.Item3 as ITreasure);
                }
                if (defender.Item4 != null && !(defender.Item4 is NoItem))
                {
                    World.AddTreasure(defender.X, defender.Y, defender.Item4 as ITreasure);
                }

                Game.Player.MonsterScore++;

                World.AddGold(defender.X, defender.Y, defender.Gold);
                World.RemoveMonster((Monster)defender);

                Game.MessageLog.Add($"The {defender.Name} died and dropped {defender.Gold} gold");
            }
        }
    }
}