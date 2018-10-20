using System;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Utilities
{
    public delegate void ItemActionEventHandler(object sender, ItemActionEventArgs e);

    public enum ItemActionType
    {
        DropFromInventory,
        DropFromEquipped,
        EquipItem,
        UnequipItem
    }

    public class ItemActionEventArgs : EventArgs
    {
        public IInventory       Item    { get; private set; }
        public ItemActionType   Action  { get; private set; }

        public ItemActionEventArgs(IInventory item, ItemActionType action)
        {
            Item    = item;
            Action  = action;
        }
    }
}