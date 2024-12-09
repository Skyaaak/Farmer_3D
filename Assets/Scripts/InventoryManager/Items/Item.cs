using System;
using UnityEngine;

namespace InventoryManager
{
    public abstract class Item: MonoBehaviour
    {
        [SerializeField] private string itemName;
        [SerializeField] private string itemDescription;
        [SerializeField] private ItemType itemType;
        [SerializeField] private float weight;
        [SerializeField] private GameObject prefab;

        public Item(string itemName, string itemDescription, ItemType itemType, float weight, GameObject prefab)
        {
            this.itemName = itemName;
            this.itemDescription = itemDescription;
            this.itemType = itemType;
            this.weight = weight;
            this.prefab = prefab;
        }
        
        public string ItemName { get; }
        public string ItemDescription { get; }
        public ItemType ItemType { get; }
        public float Weight { get; }

        public void Instantiate()
        {
            this.prefab.SetActive(true);
        }

        public void Destroy()
        {
            this.prefab.SetActive(false);
        }
        
        public abstract void UseObject(PlayerController playerController);

        public void Update()
        {
            
        }
    }
}