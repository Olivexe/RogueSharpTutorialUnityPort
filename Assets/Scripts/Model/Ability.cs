using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class Ability : IAbility, ITreasure, IDrawable
    {   
        public string   Name                { get; protected set; }
        public int      TurnsToRefresh      { get; protected set; }
        public int      TurnsUntilRefreshed { get; protected set; }
        public Actor    Owner               { get; set; }
        public          Colors Color        { get; set; }
        public char     Symbol              { get; set; }
        public int      X                   { get; set; }
        public int      Y                   { get; set; }

        protected Game  game;

        public Ability(Game game, Actor abilityOwner)
        {
            Symbol = '*';
            Color = Colors.Yellow;

            this.game = game;
            Owner = abilityOwner;
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
            if (actor != null)
            {
                if (actor.AddAbility(this))
                {
                    if (actor is Player)
                    {
                        game.MessageLog.Add($"{actor.Name} learned the {Name} ability");
                    }
                    return true;
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
