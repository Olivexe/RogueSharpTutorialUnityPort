using UnityEngine;
using UnityEngine.UI;
using RogueSharpTutorial.Model;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.View
{
    public class UI_Inventory : MonoBehaviour
    {
        [SerializeField] private Text inventoryHeadText;
        [SerializeField] private Text inventoryHandsText;
        [SerializeField] private Text inventoryFeetText;
        [SerializeField] private Text inventoryBodyText;

        [SerializeField] private Text abilityQText;
        [SerializeField] private Text abilityWText;
        [SerializeField] private Text abilityEText;
        [SerializeField] private Text abilityRText;

        [SerializeField] private Text item1Text;
        [SerializeField] private Text item2Text;
        [SerializeField] private Text item3Text;
        [SerializeField] private Text item4Text;

        /// <summary>
        /// Update inventory section of the screen for the player equipment and items.
        /// </summary>
        /// <param name="game"></param>
        public void DrawPlayerInventory(Game game)
        {
            if (game == null)
            {
                return;
            }

            inventoryHeadText.text = game.Player.Head.Name;
            inventoryHandsText.text = game.Player.Hand.Name;
            inventoryBodyText.text = game.Player.Body.Name;
            inventoryFeetText.text = game.Player.Feet.Name;

            abilityQText.text = game.Player.QAbility.Name;
            abilityWText.text = game.Player.WAbility.Name;
            abilityEText.text = game.Player.EAbility.Name;
            abilityRText.text = game.Player.RAbility.Name;

            item1Text.text = game.Player.Item1.Name;
            item2Text.text = game.Player.Item2.Name;
            item3Text.text = game.Player.Item3.Name;
            item4Text.text = game.Player.Item4.Name;


            if (game.Player.Head == HeadEquipment.None(game))
            {
                inventoryHeadText.color = ColorMap.UnityColors[Colors.DbOldStone];
            }
            else
            {
                inventoryHeadText.color = ColorMap.UnityColors[Colors.DbLight];
            }

            if (game.Player.Hand == HandEquipment.None(game))
            {
                inventoryHandsText.color = ColorMap.UnityColors[Colors.DbOldStone];
            }
            else
            {
                inventoryHandsText.color = ColorMap.UnityColors[Colors.DbLight];
            }

            if (game.Player.Body == BodyEquipment.None(game))
            {
                inventoryBodyText.color = ColorMap.UnityColors[Colors.DbOldStone];
            }
            else
            {
                inventoryBodyText.color = ColorMap.UnityColors[Colors.DbLight];
            }

            if (game.Player.Feet == FeetEquipment.None(game))
            {
                inventoryFeetText.color = ColorMap.UnityColors[Colors.DbOldStone];
            }
            else
            {
                inventoryFeetText.color = ColorMap.UnityColors[Colors.DbLight];
            }

            if (game.Player.QAbility is DoNothing)
            {
                abilityQText.color = ColorMap.UnityColors[Colors.DbOldStone];
            }
            else
            {
                abilityQText.color = ColorMap.UnityColors[Colors.DbLight];
            }

            if (game.Player.WAbility is DoNothing)
            {
                abilityWText.color = ColorMap.UnityColors[Colors.DbOldStone];
            }
            else
            {
                abilityWText.color = ColorMap.UnityColors[Colors.DbLight];
            }

            if (game.Player.EAbility is DoNothing)
            {
                abilityEText.color = ColorMap.UnityColors[Colors.DbOldStone];
            }
            else
            {
                abilityEText.color = ColorMap.UnityColors[Colors.DbLight];
            }

            if (game.Player.RAbility is DoNothing)
            {
                abilityRText.color = ColorMap.UnityColors[Colors.DbOldStone];
            }
            else
            {
                abilityRText.color = ColorMap.UnityColors[Colors.DbLight];
            }

            if (game.Player.Item1 is NoItem)
            {
                item1Text.color = ColorMap.UnityColors[Colors.DbOldStone];
            }
            else
            {
                item1Text.color = ColorMap.UnityColors[Colors.DbLight];
            }

            if (game.Player.Item2 is NoItem)
            {
                item2Text.color = ColorMap.UnityColors[Colors.DbOldStone];
            }
            else
            {
                item2Text.color = ColorMap.UnityColors[Colors.DbLight];
            }

            if (game.Player.Item3 is NoItem)
            {
                item3Text.color = ColorMap.UnityColors[Colors.DbOldStone];
            }
            else
            {
                item3Text.color = ColorMap.UnityColors[Colors.DbLight];
            }

            if (game.Player.Item4 is NoItem)
            {
                item4Text.color = ColorMap.UnityColors[Colors.DbOldStone];
            }
            else
            {
                item4Text.color = ColorMap.UnityColors[Colors.DbLight];
            }

        }
    }
}