using UnityEngine;

namespace DefaultNamespace
{
    [System.Serializable]
    public class ItemInInventory
    {
        [SerializeField] private ItemData itemData;
        [SerializeField] private int count;

        public ItemInInventory(ItemData itemData, int amount)
        {
            this.itemData = itemData;
            this.count = amount;
        }

        public ItemData getItemData()
        {
            return itemData;
        }

        public int getCount()
        {
            return count;
        }

        public void addItem()
        {
            count++;
        }

        public void addItem(int amount)
        {
            count += amount;
        }

        public void removeItem()
        {
            count--;
        }

        public void removeItem(int amount)
        {
            count -= amount;
        }
    }
}