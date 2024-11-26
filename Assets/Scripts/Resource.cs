using UnityEngine;

namespace DefaultNamespace
{
    [System.Serializable]
    public class Resource
    {
        [SerializeField] ItemData itemData;
        [SerializeField] private int minRessource;
        [SerializeField] private int maxRessource;

        public Resource(int minRessource, int maxRessource)
        {
            this.minRessource = minRessource;
            this.maxRessource = maxRessource;
        }

        public ItemData GetItemData()
        {
            return itemData;
        }

        public int GetMinResource()
        {
            return minRessource;
        }

        public int GetMaxResource()
        {
            return maxRessource;
        }
    }
}