using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class Ability : IAbility, ITreasure, IDrawable, IInventory
    {   
        public string   Name                { get; protected set; }
        public int      TurnsToRefresh      { get; protected set; }
        public int      TurnsUntilRefreshed { get; protected set; }
        public Actor    Owner               { get; set; }
        public          Colors Color        { get; set; }
        public char     Symbol              { get; set; }
        public int      X                   { get; set; }
        public int      Y                   { get; set; }
        public int      MaxStack            { get; set; }
        public int      CurrentStackAmount  { get; set; }

        protected Game  game;

        public Ability(Game game, Actor abilityOwner)
        {
            Symbol = '*';
            Color = Colors.Yellow;

            this.game = game;
            Owner = abilityOwner;
        }

        public string GetName()
        {
            return Name;
        }

        public bool Perform()
        {
            if (TurnsUntilRefreshed > 0)
            {
                game.MessageLog.Add($"{Name} is not ready to use.");
                return false;
            }

            TurnsUntilRefreshed = TurnsToRefresh;

            return PerformAbility();
        }

        protected virtual bool PerformAbility()
        {
            return false;
        }

        public void Tick()
        {
            if (TurnsUntilRefreshed > 0)
            {
                TurnsUntilRefreshed--;
            }
        }

        public bool PickUp(Actor actor)
        {
            //if (actor != null)
            //{
            //    if (actor.AddAbility(this))
            //    {
            //        if (actor is Player)
            //        {
            //            game.MessageLog.Add($"{actor.Name} learned the {Name} ability");
            //        }
            //        return true;
            //    }
            //}
            if (actor != null)
            {
                if (actor.AddToInventory(this))
                {
                    if (actor is Player)
                    {
                        game.MessageLog.Add($"You picked up {Name}.");
                    }
                    return true;
                }
                else
                {
                    if (actor is Player)
                    {
                        game.MessageLog.Add($"You can't pick up {Name}.");
                    }
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
