using UnityEngine;

namespace InventoryManager
{
    public class HarvestableItem: UseItem
    {
        public void Use(PlayerController player)
        {
            Debug.Log("Use HarvestableItem");
        }
    }
}