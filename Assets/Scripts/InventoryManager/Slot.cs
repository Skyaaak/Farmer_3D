using System;
using UnityEngine;

namespace InventoryManager
{
    public class Slot
    {
        [SerializeField] Item item;
        
        private int amount;
        private float weight;

        public Slot(Item item)
        {
            this.item = item;
            this.amount = 1;
            ActualizeWeight();
        }

        public Slot(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
            ActualizeWeight();
        }
        
        public Item Item => item;
        public int Amount => amount;
        public float Weight => weight;

        public void AddAmount(int amount)
        {
            this.amount += amount;
        }

        public void DecreaseAmount(int amount)
        {
            if (amount < 0)
            {
                throw new System.ArgumentException("Amount cannot be negative");
            }

            if (this.amount - amount < 0)
            {
                throw new System.ArgumentException("Amount cannot be negative, " + this.amount + " - " + amount + " is negative");
            }
            
            this.amount -= amount;
        }

        public void ActualizeWeight()
        {
            this.weight = this.item.Weight * this.amount;
        }
    }
}