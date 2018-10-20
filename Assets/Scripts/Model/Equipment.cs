using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class Equipment : IEquipment, ITreasure, IDrawable, IInventory
    {
        // IEquipment Properties
        public int      Attack          { get; set; }
        public int      AttackChance    { get; set; }
        public int      Awareness       { get; set; }
        public int      Defense         { get; set; }
        public int      DefenseChance   { get; set; }
        public int      Gold            { get; set; }
        public int      Health          { get; set; }
        public int      MaxHealth       { get; set; }
        public string   Name            { get; set; }
        public int      Speed           { get; set; }

        // IDrawable Properties
        public Colors   Color           { get; set; }
        public char     Symbol          { get; set; }
        public int      X               { get; set; }
        public int      Y               { get; set; }

        // IInventory Properties
        public int      MaxStack        { get; set; }
        public int      CurrentStackAmount { get; set; }

        protected Game game;

        public Equipment(Game game)
        {
            Symbol      = ']';
            Color       = Colors.Yellow;
            this.game   = game;
        }

        public string GetName()
        {
            return Name;
        }

        protected bool Equals(Equipment other)
        {
            return Attack           == other.Attack &&
                   AttackChance     == other.AttackChance &&
                   Awareness        == other.Awareness &&
                   Defense          == other.Defense &&
                   DefenseChance    == other.DefenseChance &&
                   Gold             == other.Gold &&
                   Health           == other.Health &&
                   MaxHealth        == other.MaxHealth &&
                   Speed            == other.Speed &&
                   MaxStack         == other.MaxStack &&
                   string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((Equipment)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 13;
                hashCode = (hashCode * 397) ^ Attack;
                hashCode = (hashCode * 397) ^ AttackChance;
                hashCode = (hashCode * 397) ^ Awareness;
                hashCode = (hashCode * 397) ^ Defense;
                hashCode = (hashCode * 397) ^ DefenseChance;
                hashCode = (hashCode * 397) ^ Gold;
                hashCode = (hashCode * 397) ^ Health;
                hashCode = (hashCode * 397) ^ MaxHealth;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Speed;
                return hashCode;
            }
        }

        public static bool operator ==(Equipment left, Equipment right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Equipment left, Equipment right)
        {
            return !Equals(left, right);
        }

        public bool PickUp(Actor actor)
        {
            if (actor.AddToInventory(this))
            {
                if (this is HeadEquipment && actor is Player)
                {
                    //actor.Head = this as HeadEquipment;
                    game.MessageLog.Add($"{actor.Name} picked up a {Name} helmet.");
                }

                if (this is BodyEquipment && actor is Player)
                {
                    //actor.Body = this as BodyEquipment;
                    game.MessageLog.Add($"{actor.Name} picked up {Name} body armor.");
                }

                if (this is HandsEquipment && actor is Player)
                {
                    //actor.Hand = this as HandEquipment;
                    game.MessageLog.Add($"{actor.Name} picked up the {Name} gloves.");
                }

                if (this is FeetEquipment && actor is Player)
                {
                    //actor.Feet = this as FeetEquipment;
                    game.MessageLog.Add($"{actor.Name} picked up {Name} boots.");
                }

                if ((this is MainHandEquipment ||
                     this is RangedEquipment   ||
                     this is Ammunition)       && actor is Player)
                {
                    //actor.Hand = this as HandEquipment;
                    game.MessageLog.Add($"{actor.Name} picked up the {Name}.");
                }

                return true;
            }
            else
            {
                if (this is HeadEquipment && actor is Player)
                {
                    //actor.Head = this as HeadEquipment;
                    game.MessageLog.Add($"You can't pick up a {Name} helmet.");
                }

                if (this is BodyEquipment && actor is Player)
                {
                    //actor.Body = this as BodyEquipment;
                    game.MessageLog.Add($"You can't pick up {Name} body armor.");
                }

                if (this is HandsEquipment && actor is Player)
                {
                    game.MessageLog.Add($"You can't pick up the {Name} gloves.");
                }

                if (this is FeetEquipment && actor is Player)
                {
                    game.MessageLog.Add($"You can't pick up {Name} boots.");
                }

                if ((this is MainHandEquipment ||
                     this is RangedEquipment   || 
                     this is Ammunition)       && actor is Player)
                { 
                    game.MessageLog.Add($"You can't pick up the {Name}.");
                }

            }
            return false;
        }

        public void Draw(IMap map)
        {
            if (!map.IsExplored(X, Y))
            {
                return;
            }

            if (map.IsInFov(X, Y))
            {
                game.SetMapCell(X, Y, Color, Colors.FloorBackground, Symbol, map.GetCell(X, Y).IsExplored);
            }
            else
            {
                game.SetMapCell(X, Y, Colors.YellowGray, Colors.FloorBackground, Symbol, map.GetCell(X, Y).IsExplored);
            }
        }
    }
}