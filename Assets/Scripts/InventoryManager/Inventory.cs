using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManager
{
    public class Inventory
    {
        private List<Slot> items;
        private int currentIndex;
        private int totalAmount;
        private float totalWeight;
        private float maxWeight;

        public Inventory()
        {
            this.items = new List<Slot>();
        }

        public void Update(float maxWeight)
        {
            this.maxWeight = maxWeight;
        }

        public void AddItem(Item item, int amount)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (item.Weight + this.totalWeight > this.maxWeight)
            {
                throw new InvalidOperationException("Cannot add more items than max weight");
            }

            Slot slotContainingItem = items.Where(slot => slot.Item == item).FirstOrDefault();
            if (slotContainingItem == null)
            {
                this.items.Add(new Slot(item));
            }
            else
            {
                slotContainingItem.AddAmount(amount);
            }
            
            this.totalWeight += item.Weight * amount;
        }

        public void RemoveItem(Item item, int amount)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            Slot slotContainingItem = items.Where(slot => slot.Item == item).FirstOrDefault();
            if (slotContainingItem == null)
            {
                throw new InvalidOperationException("Cannot remove an item that is not in the inventory");
            }

            if (slotContainingItem.Amount < amount)
            {
                throw new InvalidOperationException("Cannot remove more items than the amount");
            }
            
            slotContainingItem.DecreaseAmount(amount);
            this.totalWeight -= item.Weight * amount;
        }
    }
}