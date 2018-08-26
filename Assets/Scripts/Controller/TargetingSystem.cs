using System.Collections.Generic;
using System.Linq;
using RogueSharp;
using RogueSharpTutorial.View;
using RogueSharpTutorial.Model;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Controller
{
    public class TargetingSystem
    {
        private enum SelectionType
        {
            None    = 0,
            Target  = 1,
            Area    = 2,
            Line    = 3
        }

        public  bool            IsPlayerTargeting   { get; private set; }

        private Point           cursorPosition      = null;
        private List<Point>     selectableTargets   = new List<Point>();
        private int             currentTargetIndex;
        private ITargetable     targetable;
        private int             area;
        private SelectionType   selectionType;
        private Game            game;

        public TargetingSystem(Game game)
        {
            this.game = game;
        }

        public bool SelectMonster(ITargetable targetable)
        {
            Initialize();
            selectionType       = SelectionType.Target;
            DungeonMap map      = game.World;
            selectableTargets   = map.GetMonsterLocationsInFieldOfView().ToList();
            this.targetable     = targetable;
            cursorPosition      = selectableTargets.FirstOrDefault();

            if (cursorPosition == null)
            {
                StopTargeting();
                return false;
            }
            IsPlayerTargeting = true;
            return true;
        }

        public bool SelectArea(ITargetable targetable, int area = 0)
        {
            Initialize();
            selectionType       = SelectionType.Area;
            Player player       = game.Player;
            cursorPosition      = new Point { X = player.X, Y = player.Y };
            this.targetable     = targetable;
            this.area           = area;

            IsPlayerTargeting   = true;
            return true;
        }

        public bool SelectLine(ITargetable targetable)
        {
            Initialize();
            selectionType       = SelectionType.Line;
            Player player       = game.Player;
            cursorPosition      = new Point { X = player.X, Y = player.Y };
            this.targetable     = targetable;

            IsPlayerTargeting   = true;
            return true;
        }

        private void StopTargeting()
        {
            IsPlayerTargeting = false;
            Initialize();
        }

        private void Initialize()
        {
            cursorPosition      = null;
            selectableTargets   = new List<Point>();
            currentTargetIndex  = 0;
            area                = 0;
            targetable          = null;
            selectionType       = SelectionType.None;
        }

        public bool HandleKey(InputCommands command)
        {
            if (selectionType == SelectionType.Target)
            {
                HandleSelectableTargeting(command);
            }
            else if (selectionType == SelectionType.Area)
            {
                HandleLocationTargeting(command);
            }
            else if (selectionType == SelectionType.Line)
            {
                HandleLocationTargeting(command);
            }

            if (command == InputCommands.EnterKey)
            {
                targetable.SelectTarget(cursorPosition);
                StopTargeting();
                return true;
            }

            return false;
        }

        private void HandleSelectableTargeting(InputCommands command)
        {
            if (command == InputCommands.Right || command == InputCommands.Down)
            {
                currentTargetIndex++;
                if (currentTargetIndex >= selectableTargets.Count)
                {
                    currentTargetIndex = 0;
                }
            }
            else if (command == InputCommands.Left || command == InputCommands.Up)
            {
                currentTargetIndex--;
                if (currentTargetIndex < 0)
                {
                    currentTargetIndex = selectableTargets.Count - 1;
                }
            }
            
            cursorPosition = selectableTargets[currentTargetIndex];
        }

        private void HandleLocationTargeting(InputCommands command)
        {
            int x = cursorPosition.X;
            int y = cursorPosition.Y;

            DungeonMap map = game.World;

            if (command == InputCommands.Right)
            {
                x++;
            }
            else if (command == InputCommands.Left)
            {
                x--;
            }
            else if (command == InputCommands.Down)
            {
                y--;
            }
            else if (command == InputCommands.Up)
            {
                y++;
            }

            if (map.IsInFov(x, y))
            {
                cursorPosition.X = x;
                cursorPosition.Y = y;
            }
        }

        public void Draw()
        {
            if (IsPlayerTargeting)
            {
                DungeonMap map  = game.World;
                Player player   = game.Player;

                if (selectionType == SelectionType.Area)
                {
                    foreach (Cell cell in map.GetCellsInSquare(cursorPosition.X, cursorPosition.Y, area))
                    {
                        game.SetMapCellBackground(cell.X, cell.Y, Colors.DbSun);
                    }
                }
                else if (selectionType == SelectionType.Line)
                {
                    foreach (Cell cell in map.GetCellsAlongLine(player.X, player.Y, cursorPosition.X, cursorPosition.Y))
                    {
                        game.SetMapCellBackground(cell.X, cell.Y, Colors.DbSun);
                    }
                }

                game.SetMapCellBackground(cursorPosition.X, cursorPosition.Y, Colors.DbLight);
            }
        }
    }
}
