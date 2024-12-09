using System;
using UnityEngine;

namespace InventoryManager
{
    public class ItemType: MonoBehaviour
    {
        [SerializeField] protected ItemTypeData itemData;
        [SerializeField] protected UseItem useItem;
        [SerializeField] protected PlayerController playerController;

        public String GetItemType()
        {
            return itemData.ItemTypeName;
        }

        public String GetItemTypeDescription()
        {
            return itemData.Description;
        }

        public Sprite GetItemIcon()
        {
            return itemData.Visuel;
        }

        public void UseItem(PlayerController playerController)
        {
            this.useItem.Use(playerController);
        }
    }
}