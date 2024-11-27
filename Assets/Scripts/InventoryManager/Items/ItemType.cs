using System;
using UnityEngine;

namespace InventoryManager
{
    public class ItemType
    {
        [SerializeField] protected ItemTypeData itemData;

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
    }
}