using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using RogueSharpTutorial.Model;
using RogueSharpTutorial.Model.Interfaces;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Utilities;

namespace RogueSharpTutorial.View
{
    public class UI_Inventory : MonoBehaviour
    {
        private enum KeyModes
        {
            ItemSelect,
            OperationOnEquip,
            OperationOnInventory
        }

        [SerializeField] private UI_Main    rootConsole;
        [SerializeField] private GameObject confirmationBox;
        [SerializeField] private GameObject thisWindow;

        [SerializeField] private GameObject item1Key;
        [SerializeField] private GameObject item2Key;
        [SerializeField] private GameObject item3Key;
        [SerializeField] private GameObject item4Key;
        [SerializeField] private GameObject item5Key;
        [SerializeField] private GameObject item6Key;
        [SerializeField] private GameObject item7Key;
        [SerializeField] private GameObject item8Key;
        [SerializeField] private GameObject item9Key;
        [SerializeField] private GameObject item0Key;
        [SerializeField] private Text       item1Name;
        [SerializeField] private Text       item2Name;
        [SerializeField] private Text       item3Name;
        [SerializeField] private Text       item4Name;
        [SerializeField] private Text       item5Name;
        [SerializeField] private Text       item6Name;
        [SerializeField] private Text       item7Name;
        [SerializeField] private Text       item8Name;
        [SerializeField] private Text       item9Name;
        [SerializeField] private Text       item0Name;

        [SerializeField] private GameObject armorHeadKey;
        [SerializeField] private GameObject armorBodyKey;
        [SerializeField] private GameObject armorFeetKey;
        [SerializeField] private GameObject armorHandsKey;
        [SerializeField] private Text       armorHeadText;
        [SerializeField] private Text       armorBodyText;
        [SerializeField] private Text       armorFeetText;
        [SerializeField] private Text       armorHandsText;
        [SerializeField] private Text       armorBodyDefText;
        [SerializeField] private Text       armorFeetDefText;
        [SerializeField] private Text       armorHeadDefText;
        [SerializeField] private Text       armorHandsDefText;
        [SerializeField] private Text       armorBodyDefChanceText;
        [SerializeField] private Text       armorFeetDefChanceText;
        [SerializeField] private Text       armorHeadDefChanceText;
        [SerializeField] private Text       armorHandsDefChanceText;
        [SerializeField] private Text       baseDefChanceText;
        [SerializeField] private Text       baseDefText;
        [SerializeField] private Text       totalDefChanceText;
        [SerializeField] private Text       totalDefText;

        [SerializeField] private GameObject weaponMainHandKey;
        [SerializeField] private GameObject weaponRangedKey;
        [SerializeField] private GameObject weaponAmmoKey;
        [SerializeField] private Text weaponMainHandText;
        [SerializeField] private Text weaponRangedText;
        [SerializeField] private Text weaponAmmoText;
        [SerializeField] private Text weaponMainhandAttText;
        [SerializeField] private Text weaponRangedAttText;
        [SerializeField] private Text weaponAmmoAttText;
        [SerializeField] private Text weaponMainHandAttChanceText;
        [SerializeField] private Text weaponRangedAttChanceText;
        [SerializeField] private Text weaponAmmoAttChanceText;
        [SerializeField] private Text baseAttChanceText;
        [SerializeField] private Text baseAttText;
        [SerializeField] private Text totalMeleeAttChanceText;
        [SerializeField] private Text totalMeleeAttText;
        [SerializeField] private Text totalRangedAttChanceText;
        [SerializeField] private Text totalRangedAttText;

        [SerializeField] private GameObject yKey;
        [SerializeField] private GameObject uKey;
        [SerializeField] private GameObject oKey;
        [SerializeField] private GameObject lKey;
        [SerializeField] private GameObject mKey;
        [SerializeField] private GameObject bKey;
        [SerializeField] private GameObject equipText;
        [SerializeField] private GameObject unequipText;
        [SerializeField] private GameObject dropText;
        [SerializeField] private GameObject useText;
        [SerializeField] private GameObject learnText;
        [SerializeField] private GameObject cancelText;

        [SerializeField] private Text itemSelectedText;

        private IInventory itemSelected;
        private KeyModes keyMode;

        private void Start()
        {
            keyMode = KeyModes.ItemSelect;
        }

        private void Update()
        {
            if(thisWindow.activeSelf)
            {
                DrawInventory();
            }
        }

        public InputCommands GetInput()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                rootConsole.CloseAllModalWindows();
                confirmationBox.SetActive(false);
                keyMode = KeyModes.ItemSelect;
                itemSelected = null;
                return InputCommands.None;
            }

            if (keyMode == KeyModes.OperationOnEquip)
            {
                return InputOperationOnEquip();
            }
            else if (keyMode == KeyModes.OperationOnInventory)
            {
                return InputOperationOnInventory();
            }
            else if (keyMode == KeyModes.ItemSelect)
            {
                return InputItemSelection();
            }

            return InputCommands.None;
        }

        private InputCommands InputItemSelection()
        {
            if(Input.GetKeyUp(KeyCode.Alpha1))
            {
                if(rootConsole.Game.Player.Inventory != null && rootConsole.Game.Player.Inventory.Count >= 1)
                {
                    itemSelected = rootConsole.Game.Player.Inventory[0];
                    keyMode = KeyModes.OperationOnInventory;
                }
            }
            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                if (rootConsole.Game.Player.Inventory != null && rootConsole.Game.Player.Inventory.Count >= 2)
                {
                    itemSelected = rootConsole.Game.Player.Inventory[1];
                    keyMode = KeyModes.OperationOnInventory;
                }
            }
            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                if (rootConsole.Game.Player.Inventory != null && rootConsole.Game.Player.Inventory.Count >= 3)
                {
                    itemSelected = rootConsole.Game.Player.Inventory[2];
                    keyMode = KeyModes.OperationOnInventory;
                }
            }
            if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                if (rootConsole.Game.Player.Inventory != null && rootConsole.Game.Player.Inventory.Count >= 4)
                {
                    itemSelected = rootConsole.Game.Player.Inventory[3];
                    keyMode = KeyModes.OperationOnInventory;
                }
            }
            if (Input.GetKeyUp(KeyCode.Alpha5))
            {
                if (rootConsole.Game.Player.Inventory != null && rootConsole.Game.Player.Inventory.Count >= 5)
                {
                    itemSelected = rootConsole.Game.Player.Inventory[4];
                    keyMode = KeyModes.OperationOnInventory;
                }
            }
            if (Input.GetKeyUp(KeyCode.Alpha6))
            {
                if (rootConsole.Game.Player.Inventory != null && rootConsole.Game.Player.Inventory.Count >= 6)
                {
                    itemSelected = rootConsole.Game.Player.Inventory[5];
                    keyMode = KeyModes.OperationOnInventory;
                }
            }
            if (Input.GetKeyUp(KeyCode.Alpha7))
            {
                if (rootConsole.Game.Player.Inventory != null && rootConsole.Game.Player.Inventory.Count >= 7)
                {
                    itemSelected = rootConsole.Game.Player.Inventory[6];
                    keyMode = KeyModes.OperationOnInventory;
                }
            }
            if (Input.GetKeyUp(KeyCode.Alpha8))
            {
                if (rootConsole.Game.Player.Inventory != null && rootConsole.Game.Player.Inventory.Count >= 8)
                {
                    itemSelected = rootConsole.Game.Player.Inventory[7];
                    keyMode = KeyModes.OperationOnInventory;
                }
            }
            if (Input.GetKeyUp(KeyCode.Alpha9))
            {
                if (rootConsole.Game.Player.Inventory != null && rootConsole.Game.Player.Inventory.Count >= 9)
                {
                    itemSelected = rootConsole.Game.Player.Inventory[8];
                    keyMode = KeyModes.OperationOnInventory;
                }
            }
            if (Input.GetKeyUp(KeyCode.Alpha0))
            {
                if (rootConsole.Game.Player.Inventory != null && rootConsole.Game.Player.Inventory.Count >= 10)
                {
                    itemSelected = rootConsole.Game.Player.Inventory[9];
                    keyMode = KeyModes.OperationOnInventory;
                }
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                if (!(rootConsole.Game.Player.Head is HeadEquipment))
                {
                    itemSelected = rootConsole.Game.Player.Head;
                    keyMode = KeyModes.OperationOnEquip;
                }
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                if (!(rootConsole.Game.Player.Body is BodyEquipment))
                {
                    itemSelected = rootConsole.Game.Player.Body;
                    keyMode = KeyModes.OperationOnEquip;
                }
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (!(rootConsole.Game.Player.Hands is HandsEquipment))
                {
                    itemSelected = rootConsole.Game.Player.Hands;
                    keyMode = KeyModes.OperationOnEquip;
                }
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                if (!(rootConsole.Game.Player.Feet is FeetEquipment))
                {
                    itemSelected = rootConsole.Game.Player.Feet;
                    keyMode = KeyModes.OperationOnEquip;
                }
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                if (!(rootConsole.Game.Player.MainHand is MainHandEquipment))
                {
                    itemSelected = rootConsole.Game.Player.MainHand;
                    keyMode = KeyModes.OperationOnEquip;
                }
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                if (!(rootConsole.Game.Player.Ranged is RangedEquipment))
                {
                    itemSelected = rootConsole.Game.Player.Ranged;
                    keyMode = KeyModes.OperationOnEquip;
                }
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                if (!(rootConsole.Game.Player.AmmoCarried is Ammunition))
                {
                    itemSelected = rootConsole.Game.Player.AmmoCarried;
                    keyMode = KeyModes.OperationOnEquip;
                }
            }
            return InputCommands.None;
        }

        private InputCommands InputOperationOnInventory()
        {
            if (Input.GetKeyUp(KeyCode.B))
            {            
                itemSelected = null;
                keyMode = KeyModes.ItemSelect;
            }

            if (Input.GetKeyUp(KeyCode.O))
            {
                rootConsole.ManageItem(itemSelected, ItemActionType.DropFromInventory);
                itemSelected = null;
                keyMode = KeyModes.ItemSelect;
                rootConsole.CloseAllModalWindows();
                confirmationBox.SetActive(false);
                return InputCommands.DropItem;
            }

            if (Input.GetKeyUp(KeyCode.Y))
            {
                if(itemSelected is IItem || itemSelected is IAbility)
                {
                    return InputCommands.None;
                }

                if((itemSelected is HeadEquipment && rootConsole.Game.Player.Head == HeadEquipment.None(rootConsole.Game)) ||
                   (itemSelected is BodyEquipment && rootConsole.Game.Player.Head == BodyEquipment.None(rootConsole.Game)) ||
                   (itemSelected is FeetEquipment && rootConsole.Game.Player.Head == FeetEquipment.None(rootConsole.Game)) ||
                   (itemSelected is HandsEquipment && rootConsole.Game.Player.Head == HandsEquipment.None(rootConsole.Game)) ||
                   (itemSelected is MainHandEquipment && rootConsole.Game.Player.Head == MainHandEquipment.None(rootConsole.Game)) ||
                   (itemSelected is RangedEquipment && rootConsole.Game.Player.Head == RangedEquipment.None(rootConsole.Game)) ||
                   (itemSelected is Ammunition && rootConsole.Game.Player.Head == Ammunition.None(rootConsole.Game)))
                {
                    rootConsole.ManageItem(itemSelected, ItemActionType.EquipItem);
                    itemSelected = null;
                    keyMode = KeyModes.ItemSelect;
                    rootConsole.CloseAllModalWindows();
                    confirmationBox.SetActive(false);
                    return InputCommands.DropItem;
                }
                
            }

            return InputCommands.None;
        }

        private InputCommands InputOperationOnEquip()
        {
            return InputCommands.None;
        }

        private void DrawInventory()
        {
            DrawEquippedArmor();
            DrawEquippedWeapons();
            DrawLooseInventory();
            DrawSelectionOptions();
        }

        private void DrawSelectionOptions()
        {
            if(keyMode == KeyModes.OperationOnEquip)
            {
                oKey.SetActive(true);
                dropText.SetActive(true);

                uKey.SetActive(true);
                unequipText.SetActive(true);

                bKey.SetActive(true);
                cancelText.SetActive(true);

                itemSelectedText.text = ((IEquipment)itemSelected).Name;
            }
            else if(keyMode == KeyModes.OperationOnInventory)
            {
                oKey.SetActive(true);
                dropText.SetActive(true);

                bKey.SetActive(true);
                cancelText.SetActive(true);

                if (itemSelected is IEquipment)
                {
                    yKey.SetActive(true);
                    equipText.SetActive(true);
                    itemSelectedText.text = ((IEquipment)itemSelected).Name;
                }
                if (itemSelected is IItem)
                {
                    mKey.SetActive(true);
                    useText.SetActive(true);
                    itemSelectedText.text = ((IItem)itemSelected).Name;
                }
                if (itemSelected is IAbility)
                {
                    lKey.SetActive(true);
                    learnText.SetActive(true);
                    itemSelectedText.text = ((IAbility)itemSelected).Name;
                }
            }
            else
            {
                yKey.SetActive(false);
                uKey.SetActive(false);
                oKey.SetActive(false);
                lKey.SetActive(false);
                mKey.SetActive(false);
                bKey.SetActive(false);
                cancelText.SetActive(false);
                equipText.SetActive(false);
                unequipText.SetActive(false);
                useText.SetActive(false);
                learnText.SetActive(false);
                dropText.SetActive(false);

                itemSelectedText.text = "";
            }
        }

        private void DrawLooseInventory()
        {
            item1Key.SetActive(false);
            item2Key.SetActive(false);
            item3Key.SetActive(false);
            item4Key.SetActive(false);
            item5Key.SetActive(false);
            item6Key.SetActive(false);
            item7Key.SetActive(false);
            item8Key.SetActive(false);
            item9Key.SetActive(false);
            item0Key.SetActive(false);

            item1Name.text = "";
            item2Name.text = "";
            item3Name.text = "";
            item4Name.text = "";
            item5Name.text = "";
            item6Name.text = "";
            item7Name.text = "";
            item8Name.text = "";
            item9Name.text = "";
            item0Name.text = "";

            List<IInventory> items = rootConsole.Game.Player.Inventory;

            if (items == null)
            {
                return;
            }

            if (items.Count >= 1)
            {
                item1Name.text = ((ITreasure)items[0]).GetName();
                if(keyMode == KeyModes.ItemSelect)
                {
                    item1Key.SetActive(true);
                }
                else
                {
                    item1Key.SetActive(false);
                }
            }
            if (items.Count >= 2)
            {
                item2Name.text = ((ITreasure)items[1]).GetName();
                if (keyMode == KeyModes.ItemSelect)
                {
                    item2Key.SetActive(true);
                }
                else
                {
                    item2Key.SetActive(false);
                }
            }
            if (items.Count >= 3)
            {
                item3Name.text = ((ITreasure)items[2]).GetName();
                if (keyMode == KeyModes.ItemSelect)
                {
                    item3Key.SetActive(true);
                }
                else
                {
                    item3Key.SetActive(false);
                }
            }
            if (items.Count >= 4)
            {
                item4Name.text = ((ITreasure)items[3]).GetName();
                if (keyMode == KeyModes.ItemSelect)
                {
                    item4Key.SetActive(true);
                }
                else
                {
                    item4Key.SetActive(false);
                }
            }
            if (items.Count >= 5)
            {
                item5Name.text = ((ITreasure)items[4]).GetName();
                if (keyMode == KeyModes.ItemSelect)
                {
                    item5Key.SetActive(true);
                }
                else
                {
                    item5Key.SetActive(false);
                }
            }
            if (items.Count >= 6)
            {
                item6Name.text = ((ITreasure)items[5]).GetName();
                if (keyMode == KeyModes.ItemSelect)
                {
                    item6Key.SetActive(true);
                }
                else
                {
                    item6Key.SetActive(false);
                }
            }
            if (items.Count >= 7)
            {
                item7Name.text = ((ITreasure)items[6]).GetName();
                if (keyMode == KeyModes.ItemSelect)
                {
                    item7Key.SetActive(true);
                }
                else
                {
                    item7Key.SetActive(false);
                }
            }
            if (items.Count >= 8)
            {
                item8Name.text = ((ITreasure)items[7]).GetName();
                if (keyMode == KeyModes.ItemSelect)
                {
                    item8Key.SetActive(true);
                }
                else
                {
                    item8Key.SetActive(false);
                }
            }
            if (items.Count >= 9)
            {
                item9Name.text = ((ITreasure)items[8]).GetName();
                if (keyMode == KeyModes.ItemSelect)
                {
                    item9Key.SetActive(true);
                }
                else
                {
                    item9Key.SetActive(false);
                }
            }
            if (items.Count >= 10)
            {
                item0Name.text = ((ITreasure)items[9]).GetName();
                if (keyMode == KeyModes.ItemSelect)
                {
                    item0Key.SetActive(true);
                }
                else
                {
                    item0Key.SetActive(false);
                }
            }
        }

        private void DrawEquippedWeapons()
        {
            weaponMainHandKey.SetActive(false);
            weaponRangedKey.SetActive(false);
            weaponAmmoKey.SetActive(false);
            weaponMainHandText.text = "";
            weaponRangedText.text = "";
            weaponAmmoText.text = "";
            weaponMainhandAttText.text = "";
            weaponRangedAttText.text = "";
            weaponAmmoAttText.text = "";
            weaponMainHandAttChanceText.text = "";
            weaponRangedAttChanceText.text = "";
            weaponAmmoAttChanceText.text = "";
            baseAttChanceText.text = "";
            baseAttText.text = "";
            totalMeleeAttChanceText.text = "";
            totalMeleeAttText.text = "";
            totalRangedAttChanceText.text = "";
            totalRangedAttText.text = "";

            baseAttText.text = rootConsole.Game.Player.AttackBase.ToString();
            baseAttChanceText.text = rootConsole.Game.Player.AttackChanceBase.ToString() + "%";

            totalMeleeAttText.text = rootConsole.Game.Player.AttackMeleeAdjusted.ToString();
            totalMeleeAttChanceText.text = rootConsole.Game.Player.AttackChanceMeleeAdjusted.ToString() + "%";
            totalRangedAttText.text = rootConsole.Game.Player.AttackRangedAdjusted.ToString();
            totalRangedAttChanceText.text = rootConsole.Game.Player.AttackChanceRangedAdjusted.ToString() + "%";

            if (rootConsole.Game.Player.MainHand == MainHandEquipment.None(rootConsole.Game))
            {
                weaponMainHandText.text = "";
                weaponMainHandKey.SetActive(false);
                weaponMainhandAttText.text = "";
                weaponMainHandAttChanceText.text = "";
            }
            else
            {
                weaponMainHandText.text = rootConsole.Game.Player.MainHand.Name;
                weaponMainHandKey.SetActive(true);
                weaponMainhandAttText.text = rootConsole.Game.Player.MainHand.Attack.ToString();
                weaponMainHandAttChanceText.text = rootConsole.Game.Player.MainHand.AttackChance.ToString();
            }

            if (rootConsole.Game.Player.Ranged == RangedEquipment.None(rootConsole.Game))
            {
                weaponRangedText.text = "";
                weaponRangedKey.SetActive(false);
                weaponRangedAttText.text = "";
                weaponRangedAttChanceText.text = "";
            }
            else
            {
                weaponRangedText.text = rootConsole.Game.Player.Ranged.Name;
                weaponRangedKey.SetActive(true);
                weaponRangedAttText.text = rootConsole.Game.Player.Ranged.Attack.ToString();
                weaponRangedAttChanceText.text = rootConsole.Game.Player.Ranged.AttackChance.ToString();
            }

            if (rootConsole.Game.Player.AmmoCarried == Ammunition.None(rootConsole.Game))
            {
                weaponAmmoText.text = "";
                weaponAmmoKey.SetActive(false);
                weaponAmmoAttText.text = "";
                weaponAmmoAttChanceText.text = "";
            }
            else
            {
                weaponAmmoText.text = rootConsole.Game.Player.AmmoCarried.Name;
                weaponAmmoKey.SetActive(true);
                weaponAmmoAttText.text = rootConsole.Game.Player.AmmoCarried.Attack.ToString();
                weaponAmmoAttChanceText.text = rootConsole.Game.Player.AmmoCarried.AttackChance.ToString();
            }
        }

        private void DrawEquippedArmor()
        {
            armorBodyText.text = "";
            armorFeetText.text = "";
            armorHeadText.text = "";
            armorHandsText.text = "";
            armorBodyDefText.text = "";
            armorFeetDefText.text = "";
            armorHeadDefText.text = "";
            armorHandsDefText.text = "";
            armorBodyDefChanceText.text = "";
            armorFeetDefChanceText.text = "";
            armorHeadDefChanceText.text = "";
            armorHandsDefChanceText.text = "";
            armorHeadKey.SetActive(false);
            armorHandsKey.SetActive(false);
            armorBodyKey.SetActive(false);
            armorFeetKey.SetActive(false);

            baseDefText.text = rootConsole.Game.Player.DefenseBase.ToString();
            baseDefChanceText.text = rootConsole.Game.Player.DefenseChanceBase.ToString() + "%";
            totalDefText.text = rootConsole.Game.Player.DefenseAdjusted.ToString();
            totalDefChanceText.text = rootConsole.Game.Player.DefenseChanceAdjusted.ToString() + "%";

            if (rootConsole.Game.Player.Head == HeadEquipment.None(rootConsole.Game))
            {
                armorHeadText.text = "";
                armorHeadKey.SetActive(false);
                armorHeadDefText.text = "";
                armorHeadDefChanceText.text = "";
            }
            else
            {
                armorHeadText.text = rootConsole.Game.Player.Head.Name;
                armorHeadKey.SetActive(true);
                armorHeadDefText.text = rootConsole.Game.Player.Head.Defense.ToString();
                armorHeadDefChanceText.text = rootConsole.Game.Player.Head.DefenseChance.ToString();
            }

            if (rootConsole.Game.Player.Feet == FeetEquipment.None(rootConsole.Game))
            {
                armorFeetText.text = "";
                armorFeetKey.SetActive(false);
                armorFeetDefText.text = "";
                armorFeetDefChanceText.text = "";
            }
            else
            {
                armorFeetText.text = rootConsole.Game.Player.Feet.Name;
                armorFeetKey.SetActive(true);
                armorFeetDefText.text = rootConsole.Game.Player.Feet.Defense.ToString();
                armorFeetDefChanceText.text = rootConsole.Game.Player.Feet.DefenseChance.ToString();
            }

            if (rootConsole.Game.Player.Body == BodyEquipment.None(rootConsole.Game))
            {
                armorBodyText.text = "";
                armorBodyKey.SetActive(false);
                armorBodyDefText.text = "";
                armorBodyDefChanceText.text = "";
            }
            else
            {
                armorBodyText.text = rootConsole.Game.Player.Body.Name;
                armorBodyKey.SetActive(true);
                armorBodyDefText.text = rootConsole.Game.Player.Body.Defense.ToString();
                armorBodyDefChanceText.text = rootConsole.Game.Player.Body.DefenseChance.ToString();
            }

            if (rootConsole.Game.Player.Hands == HandsEquipment.None(rootConsole.Game))
            {
                armorHandsText.text = "";
                armorHandsKey.SetActive(false);
                armorHandsDefText.text = "";
                armorHandsDefChanceText.text = "";
            }
            else
            {
                armorHandsText.text = rootConsole.Game.Player.Hands.Name;
                armorHandsKey.SetActive(true);
                armorHandsDefText.text = rootConsole.Game.Player.Hands.Defense.ToString();
                armorHandsDefChanceText.text = rootConsole.Game.Player.Hands.DefenseChance.ToString();
            }
        }

        private void EnableConfirmationBox(bool active)
        {
            confirmationBox.SetActive(active);
        }
    }
}