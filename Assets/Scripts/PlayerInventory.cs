using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerInventory: MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        private void Start()
        {
            inventory = new Inventory();
        }

        public void AddItem(Item item)
        {
            inventory.AddItem(item);
        }
        
        
    }
}