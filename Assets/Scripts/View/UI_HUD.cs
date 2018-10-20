using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using RogueSharpTutorial.Model;
using RogueSharpTutorial.Model.Interfaces;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.View
{
    public class UI_HUD : MonoBehaviour
    {
        [SerializeField] private Text inventoryHeadText;
        [SerializeField] private Text inventoryHandsText;
        [SerializeField] private Text inventoryFeetText;
        [SerializeField] private Text inventoryBodyText;

        [SerializeField] private Text abilityQText;
        [SerializeField] private Text abilityWText;
        [SerializeField] private Text abilityEText;
        [SerializeField] private Text abilityRText;
        [SerializeField] private Text abilityQReuseText;
        [SerializeField] private Text abilityWReuseText;
        [SerializeField] private Text abilityEReuseText;
        [SerializeField] private Text abilityRReuseText;

        [SerializeField] private Text item1Text;
        [SerializeField] private Text item2Text;
        [SerializeField] private Text item3Text;
        [SerializeField] private Text item4Text;
        [SerializeField] private Text item1UsesText;
        [SerializeField] private Text item2UsesText;
        [SerializeField] private Text item3UsesText;
        [SerializeField] private Text item4UsesText;

        [SerializeField] private Text effect1Text;
        [SerializeField] private Text effect2Text;
        [SerializeField] private Text effect3Text;
        [SerializeField] private Text effect4Text;
        [SerializeField] private Text effect1DurationText;
        [SerializeField] private Text effect2DurationText;
        [SerializeField] private Text effect3DurationText;
        [SerializeField] private Text effect4DurationText;

        [SerializeField] private Text item1OngroundText;
        [SerializeField] private Text item2OngroundText;
        [SerializeField] private Text item3OngroundText;
        [SerializeField] private Text item4OngroundText;
        [SerializeField] private GameObject item1OnGroundComGO;
        [SerializeField] private GameObject item2OnGroundComGO;
        [SerializeField] private GameObject item3OnGroundComGO;
        [SerializeField] private GameObject item4OnGroundComGO;
        [SerializeField] private GameObject grabAllText;
        [SerializeField] private GameObject grabAllCommand;
        [SerializeField] private GameObject moreItemsText;

        public bool Isitem1Onground { get; private set; }
        public bool Isitem2Onground { get; private set; }
        public bool Isitem3Onground { get; private set; }
        public bool Isitem4Onground { get; private set; }

        [SerializeField] private Text scoreMonstersKilledText;
        [SerializeField] private Text scoreGoldText;
        [SerializeField] private Text scoreLevelText;
        [SerializeField] private Text scoreTotalScore;

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

            //DrawEquipment(game);
            DrawAbilities(game);
            DrawItems(game);
            DrawEffects(game);
            DrawItemsOnGround(game);
        }

        //private void DrawEquipment(Game game)
        //{
        //    inventoryHeadText.text = game.Player.Head.Name;
        //    inventoryHandsText.text = game.Player.Hand.Name;
        //    inventoryBodyText.text = game.Player.Body.Name;
        //    inventoryFeetText.text = game.Player.Feet.Name;

        //    if (game.Player.Head == HeadEquipment.None(game))
        //    {
        //        inventoryHeadText.color = ColorMap.UnityColors[Colors.DbOldStone];
        //    }
        //    else
        //    {
        //        inventoryHeadText.color = ColorMap.UnityColors[Colors.DbLight];
        //    }

        //    if (game.Player.Hand == HandEquipment.None(game))
        //    {
        //        inventoryHandsText.color = ColorMap.UnityColors[Colors.DbOldStone];
        //    }
        //    else
        //    {
        //        inventoryHandsText.color = ColorMap.UnityColors[Colors.DbLight];
        //    }

        //    if (game.Player.Body == BodyEquipment.None(game))
        //    {
        //        inventoryBodyText.color = ColorMap.UnityColors[Colors.DbOldStone];
        //    }
        //    else
        //    {
        //        inventoryBodyText.color = ColorMap.UnityColors[Colors.DbLight];
        //    }

        //    if (game.Player.Feet == FeetEquipment.None(game))
        //    {
        //        inventoryFeetText.color = ColorMap.UnityColors[Colors.DbOldStone];
        //    }
        //    else
        //    {
        //        inventoryFeetText.color = ColorMap.UnityColors[Colors.DbLight];
        //    }
        //}

        private void DrawAbilities(Game game)
        {
            abilityQText.text = game.Player.QAbility.Name;
            abilityWText.text = game.Player.WAbility.Name;
            abilityEText.text = game.Player.EAbility.Name;
            abilityRText.text = game.Player.RAbility.Name;

            if (game.Player.QAbility is DoNothing)
            {
                abilityQText.color = ColorMap.UnityColors[Colors.DbOldStone];
                abilityQReuseText.text = "";
            }
            else
            {
                abilityQText.color = ColorMap.UnityColors[Colors.DbLight];
                abilityQReuseText.color = ColorMap.UnityColors[Colors.DbLight];

                if (game.Player.QAbility.TurnsUntilRefreshed > 0)
                {
                    abilityQReuseText.text = game.Player.QAbility.TurnsUntilRefreshed.ToString();
                }
                else
                {
                    abilityQReuseText.text = "Ready";
                }
            }

            if (game.Player.WAbility is DoNothing)
            {
                abilityWText.color = ColorMap.UnityColors[Colors.DbOldStone];
                abilityWReuseText.text = "";
            }
            else
            {
                abilityWText.color = ColorMap.UnityColors[Colors.DbLight];
                abilityWReuseText.color = ColorMap.UnityColors[Colors.DbLight];

                if (game.Player.WAbility.TurnsUntilRefreshed > 0)
                {
                    abilityWReuseText.text = game.Player.WAbility.TurnsUntilRefreshed.ToString();
                }
                else
                {
                    abilityWReuseText.text = "Ready";
                }
            }

            if (game.Player.EAbility is DoNothing)
            {
                abilityEText.color = ColorMap.UnityColors[Colors.DbOldStone];
                abilityEReuseText.text = "";
            }
            else
            {
                abilityEText.color = ColorMap.UnityColors[Colors.DbLight];
                abilityEReuseText.color = ColorMap.UnityColors[Colors.DbLight];

                if (game.Player.EAbility.TurnsUntilRefreshed > 0)
                {
                    abilityEReuseText.text = game.Player.EAbility.TurnsUntilRefreshed.ToString();
                }
                else
                {
                    abilityEReuseText.text = "Ready";
                }
            }

            if (game.Player.RAbility is DoNothing)
            {
                abilityRText.color = ColorMap.UnityColors[Colors.DbOldStone];
                abilityRReuseText.text = "";
            }
            else
            {
                abilityRText.color = ColorMap.UnityColors[Colors.DbLight];
                abilityRReuseText.color = ColorMap.UnityColors[Colors.DbLight];

                if (game.Player.RAbility.TurnsUntilRefreshed > 0)
                {
                    abilityRReuseText.text = game.Player.RAbility.TurnsUntilRefreshed.ToString();
                }
                else
                {
                    abilityRReuseText.text = "Ready";
                }
            }
        }

        private void DrawItems(Game game)
        {
            item1Text.text = game.Player.Item1.Name;
            item2Text.text = game.Player.Item2.Name;
            item3Text.text = game.Player.Item3.Name;
            item4Text.text = game.Player.Item4.Name;

            if (game.Player.Item1 is NoItem)
            {
                item1Text.color = ColorMap.UnityColors[Colors.DbOldStone];
                item1UsesText.text = "";
            }
            else
            {
                item1Text.color     = ColorMap.UnityColors[Colors.DbLight];
                item1UsesText.color = ColorMap.UnityColors[Colors.DbLight];
                item1UsesText.text  = game.Player.Item1.RemainingUses.ToString();
            }

            if (game.Player.Item2 is NoItem)
            {
                item2Text.color     = ColorMap.UnityColors[Colors.DbOldStone];
                item2UsesText.text  = "";
            }
            else
            {
                item2Text.color     = ColorMap.UnityColors[Colors.DbLight];
                item2UsesText.color = ColorMap.UnityColors[Colors.DbLight];
                item2UsesText.text  = game.Player.Item2.RemainingUses.ToString();
            }

            if (game.Player.Item3 is NoItem)
            {
                item3Text.color     = ColorMap.UnityColors[Colors.DbOldStone];
                item3UsesText.text  = "";
            }
            else
            {
                item3Text.color     = ColorMap.UnityColors[Colors.DbLight];
                item3UsesText.color = ColorMap.UnityColors[Colors.DbLight];
                item3UsesText.text  = game.Player.Item3.RemainingUses.ToString();
            }

            if (game.Player.Item4 is NoItem)
            {
                item4Text.color     = ColorMap.UnityColors[Colors.DbOldStone];
                item4UsesText.text  = "";
            }
            else
            {
                item4Text.color     = ColorMap.UnityColors[Colors.DbLight];
                item4UsesText.color = ColorMap.UnityColors[Colors.DbLight];
                item4UsesText.text  = game.Player.Item4.RemainingUses.ToString();
            }

            scoreMonstersKilledText.text    = game.Player.MonsterScore.ToString();
            scoreGoldText.text              = game.Player.GoldScore.ToString();
            scoreLevelText.text             = game.Player.LevelScore.ToString();
            scoreTotalScore.text            = game.Player.TotalScore.ToString();
        }

        private void DrawEffects(Game game)
        {
            effect1Text.text = "";
            effect2Text.text = "";
            effect3Text.text = "";
            effect4Text.text = "";
            effect1DurationText.text = "";
            effect2DurationText.text = "";
            effect3DurationText.text = "";
            effect4DurationText.text = "";

            if (game.Player.Effects != null || game.Player.Effects.Count > 0)
            {
                for (int i = 0; i < game.Player.Effects.Count; i++)
                {
                    if (i == 0)
                    {
                        effect1Text.text = game.Player.Effects[i].Name;
                        effect1DurationText.text = game.Player.Effects[i].Duration.ToString();
                    }
                    else if(i == 1)
                    {
                        effect2Text.text = game.Player.Effects[i].Name;
                        effect2DurationText.text = game.Player.Effects[i].Duration.ToString();
                    }
                    else if (i == 2)
                    {
                        effect3Text.text = game.Player.Effects[i].Name;
                        effect3DurationText.text = game.Player.Effects[i].Duration.ToString();
                    }
                    else if (i == 3)
                    {
                        effect4Text.text = game.Player.Effects[i].Name;
                        effect4DurationText.text = game.Player.Effects[i].Duration.ToString();
                    }

                }
            }
        }

        private void DrawItemsOnGround(Game game)
        {
            grabAllCommand.SetActive(false);
            grabAllText.SetActive(false);
            moreItemsText.SetActive(false);
            item1OnGroundComGO.SetActive(false);
            item2OnGroundComGO.SetActive(false);
            item3OnGroundComGO.SetActive(false);
            item4OnGroundComGO.SetActive(false);
            item1OngroundText.text = "";
            item2OngroundText.text = "";
            item3OngroundText.text = "";
            item4OngroundText.text = "";
            Isitem1Onground = false;
            Isitem2Onground = false;
            Isitem3Onground = false;
            Isitem4Onground = false;

            List<TreasurePile> treasures = game.World.GetAllTreasurePilesAt(game.Player.X, game.Player.Y);

            if (treasures == null)
            {
                return;
            }

            if(treasures.Count >= 1)
            {
                item1OnGroundComGO.SetActive(true);
                item1OngroundText.text = treasures[0].Treasure.GetName();
                Isitem1Onground = true;
            }
            if (treasures.Count >= 2)
            {
                item2OnGroundComGO.SetActive(true);
                item2OngroundText.text = treasures[1].Treasure.GetName();
                Isitem2Onground = true;
                grabAllCommand.SetActive(true);
                grabAllText.SetActive(true);
            }
            if (treasures.Count >= 3)
            {
                item3OnGroundComGO.SetActive(true);
                item3OngroundText.text = treasures[2].Treasure.GetName();
                Isitem3Onground = true;
            }
            if (treasures.Count >= 4)
            {
                item4OnGroundComGO.SetActive(true);
                item4OngroundText.text = treasures[3].Treasure.GetName();
                Isitem4Onground = true;
            }
            if (treasures.Count >= 4)
            {
                moreItemsText.SetActive(true);
            }
        }
    }
}