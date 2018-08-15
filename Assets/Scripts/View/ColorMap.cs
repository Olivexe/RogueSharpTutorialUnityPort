using UnityEngine;
using System.Collections.Generic;
using RogueSharpTutorial.Model;

namespace RogueSharpTutorial.View
{
    public static class ColorMap
    {
        public static Dictionary<Colors, Color> UnityColors = new Dictionary<Colors, Color>
        {
            { Colors.Clear,             Color.clear },
            { Colors.White,             Color.white },
            { Colors.Black,             Color.black },
            { Colors.FloorBackground,   Color.black },

            { Colors.Floor,             Palette.AlternateDarkest },
            { Colors.FloorBackgroundFov,Palette.DbDark },
            { Colors.FloorFov,          Palette.Alternate },

            { Colors.WallBackground,    Palette.SecondaryDarkest },
            { Colors.Wall,              Palette.Secondary },
            { Colors.WallBackgroundFov, Palette.SecondaryDarker },
            { Colors.WallFov,           Palette.SecondaryLighter },

            { Colors.Player,            Palette.DbLight },

            { Colors.TextHeading,       Color.white },
            { Colors.Text,              Palette.DbLight },
            { Colors. Gold,             Palette.DbSun },

            { Colors.KoboldColor,       Palette.DbBrightWood },

            { Colors.DoorBackground,    Palette.ComplimentDarkest },
            { Colors.Door,              Palette.ComplimentLighter },
            { Colors.DoorBackgroundFov, Palette.ComplimentDarker },
            { Colors.DoorFov,           Palette.ComplimentLightest }
        };
    }
}