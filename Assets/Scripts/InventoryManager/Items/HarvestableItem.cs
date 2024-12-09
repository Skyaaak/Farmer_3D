using UnityEngine;

namespace InventoryManager
{
    public class HarvestableItem: Item
    {
        public HarvestableItem(string itemName, string itemDescription, ItemType itemType, float weight, GameObject prefab) : base(itemName, itemDescription, itemType, weight, prefab)
        {
            
        }

        public override void UseObject(PlayerController playerController)
        {
            throw new System.NotImplementedException();
        }
    }
}