using System.Collections.Generic;
using System.Linq;
using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.View
{
    public class TargetingSystem
    {
        private enum SelectionType
        {
            None = 0,
            Target = 1,
            Area = 2,
            Line = 3
        }

        public  bool            IsPlayerTargeting   { get; private set; }

        private Point           cursorPosition;
        private List<Point>     selectableTargets   = new List<Point>();
        private int             currentTargetIndex;
        private ITargetable     targetable;
        private int             area;
        private SelectionType   selectionType;

        //    public bool SelectMonster(ITargetable targetable)
        //    {
        //        Initialize();
        //        _selectionType = SelectionType.Target;
        //        DungeonMap map = Game.DungeonMap;
        //        _selectableTargets = map.GetMonsterLocationsInFieldOfView().ToList();
        //        _targetable = targetable;
        //        _cursorPosition = _selectableTargets.FirstOrDefault();
        //        if (_cursorPosition == null)
        //        {
        //            StopTargeting();
        //            return false;
        //        }

        //        IsPlayerTargeting = true;
        //        return true;
        //    }

        //    public bool SelectArea(ITargetable targetable, int area = 0)
        //    {
        //        Initialize();
        //        _selectionType = SelectionType.Area;
        //        Player player = Game.Player;
        //        _cursorPosition = new Point { X = player.X, Y = player.Y };
        //        _targetable = targetable;
        //        _area = area;

        //        IsPlayerTargeting = true;
        //        return true;
        //    }

        //    public bool SelectLine(ITargetable targetable)
        //    {
        //        Initialize();
        //        _selectionType = SelectionType.Line;
        //        Player player = Game.Player;
        //        _cursorPosition = new Point { X = player.X, Y = player.Y };
        //        _targetable = targetable;

        //        IsPlayerTargeting = true;
        //        return true;
        //    }

        //    private void StopTargeting()
        //    {
        //        IsPlayerTargeting = false;
        //        Initialize();
        //    }

        //    private void Initialize()
        //    {
        //        _cursorPosition = null;
        //        _selectableTargets = new List<Point>();
        //        _currentTargetIndex = 0;
        //        _area = 0;
        //        _targetable = null;
        //        _selectionType = SelectionType.None;
        //    }

        //    public bool HandleKey(RLKey key)
        //    {
        //        if (_selectionType == SelectionType.Target)
        //        {
        //            HandleSelectableTargeting(key);
        //        }
        //        else if (_selectionType == SelectionType.Area)
        //        {
        //            HandleLocationTargeting(key);
        //        }
        //        else if (_selectionType == SelectionType.Line)
        //        {
        //            HandleLocationTargeting(key);
        //        }

        //        if (key == RLKey.Enter)
        //        {
        //            _targetable.SelectTarget(_cursorPosition);
        //            StopTargeting();
        //            return true;
        //        }

        //        return false;
        //    }

        //    private void HandleSelectableTargeting(RLKey key)
        //    {
        //        if (key == RLKey.Right || key == RLKey.Down)
        //        {
        //            _currentTargetIndex++;
        //            if (_currentTargetIndex >= _selectableTargets.Count)
        //            {
        //                _currentTargetIndex = 0;
        //            }
        //            _cursorPosition = _selectableTargets[_currentTargetIndex];
        //        }
        //        else if (key == RLKey.Left || key == RLKey.Up)
        //        {
        //            _currentTargetIndex--;
        //            if (_currentTargetIndex < 0)
        //            {
        //                _currentTargetIndex = _selectableTargets.Count - 1;
        //            }
        //            _cursorPosition = _selectableTargets[_currentTargetIndex];
        //        }
        //    }

        //    private void HandleLocationTargeting(RLKey key)
        //    {
        //        int x = _cursorPosition.X;
        //        int y = _cursorPosition.Y;
        //        DungeonMap map = Game.DungeonMap;

        //        if (key == RLKey.Right)
        //        {
        //            x++;
        //        }
        //        else if (key == RLKey.Left)
        //        {
        //            x--;
        //        }
        //        else if (key == RLKey.Up)
        //        {
        //            y--;
        //        }
        //        else if (key == RLKey.Down)
        //        {
        //            y++;
        //        }

        //        if (map.IsInFov(x, y))
        //        {
        //            _cursorPosition.X = x;
        //            _cursorPosition.Y = y;
        //        }
        //    }

        //    public void Draw(RLConsole mapConsole)
        //    {
        //        if (IsPlayerTargeting)
        //        {
        //            DungeonMap map = Game.DungeonMap;
        //            Player player = Game.Player;
        //            if (_selectionType == SelectionType.Area)
        //            {
        //                foreach (Cell cell in map.GetCellsInArea(_cursorPosition.X, _cursorPosition.Y, _area))
        //                {
        //                    mapConsole.SetBackColor(cell.X, cell.Y, Swatch.DbSun);
        //                }
        //            }
        //            else if (_selectionType == SelectionType.Line)
        //            {
        //                foreach (Cell cell in map.GetCellsAlongLine(player.X, player.Y, _cursorPosition.X, _cursorPosition.Y))
        //                {
        //                    mapConsole.SetBackColor(cell.X, cell.Y, Swatch.DbSun);
        //                }
        //            }

        //            mapConsole.SetBackColor(_cursorPosition.X, _cursorPosition.Y, Swatch.DbLight);
        //        }
        //    }
    }
}
