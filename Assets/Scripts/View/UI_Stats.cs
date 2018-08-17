using UnityEngine;
using UnityEngine.UI;
using RogueSharpTutorial.Model;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.View
{
    public class UI_Stats : MonoBehaviour
    {
        [SerializeField] private Text                   nameField;
        [SerializeField] private Text                   healthField;
        [SerializeField] private Text                   attackField;
        [SerializeField] private Text                   defenseField;
        [SerializeField] private Text                   goldField;
        [SerializeField] private Text                   mapLevelField;

        [SerializeField] private Text                   inventoryHeadText;
        [SerializeField] private Text                   inventoryHandsText;
        [SerializeField] private Text                   inventoryFeetText;
        [SerializeField] private Text                   inventoryBodyText;

        [SerializeField] private VerticalLayoutGroup    enemyGroup;
        [SerializeField] private GameObject             statBarPrefab;

        /// <summary>
        /// Update stats section of the screen for the player stats.
        /// </summary>
        /// <param name="game"></param>
        public void DrawPlayerStats(Game game)
        {
            if(game == null)
            {
                return;
            }

            nameField.color = healthField.color = attackField.color = defenseField.color = mapLevelField.color = ColorMap.UnityColors[Colors.Text];
            goldField.color = ColorMap.UnityColors[Colors.Gold];

            nameField.text      = "Name:    " + game.Player.Name;
            healthField.text    = "Health:   " + game.Player.Health + "/" + game.Player.MaxHealth;
            attackField.text    = "Attack:   " + game.Player.Attack + " (" + game.Player.AttackChance + "%)";
            defenseField.text   = "Defense: " + game.Player.Defense + " (" + game.Player.DefenseChance + "%)";
            goldField.text      = "Gold:      " + game.Player.Gold;
            mapLevelField.text  = "Map Level: " + game.mapLevel;
        }

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

            inventoryHeadText.text = $"Head: {game.Player.Head.Name}";
            inventoryHandsText.text = $"Weapon: {game.Player.Hand.Name}";
            inventoryBodyText.text = $"Body: {game.Player.Body.Name}";
            inventoryFeetText.text = $"Feet: {game.Player.Feet.Name}";

            if(game.Player.Head == HeadEquipment.None(game))
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
        }

        /// <summary>
        /// Update the Monsters section of stats screen dynamically based on the number of Monsters in the Player's FOV.
        /// </summary>
        /// <param name="monster"></param>
        /// <param name="position"></param>
        public void DrawMonsterStats(Monster monster, int position)
        {
            if(position == 0)
            {
                ClearMonsterStats();
            }

            GameObject obj = Instantiate(statBarPrefab);
            obj.transform.SetParent(enemyGroup.transform, false);

            StatBar bar = obj.GetComponent<StatBar>();

            bar.SetSlider(monster.Health, monster.MaxHealth, monster.Name, ColorMap.UnityColors[monster.Color], monster.Symbol);
        }

        /// <summary>
        /// Clear all Monsters from the stats section of the screen. Necessary when there was one monster in the Player's FOV and then none were.
        /// </summary>
        public void ClearMonsterStats()
        {
            foreach (Transform child in enemyGroup.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
