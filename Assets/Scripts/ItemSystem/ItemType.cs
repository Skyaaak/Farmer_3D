using UnityEngine;

namespace ItemSystem
{
    public class ItemType: ScriptableObject
    {
        public string TypeName { get; private set; }
        public Sprite Icon { get; private set; }
        
        public bool IsWearable { get; private set; }
        public bool IsStockable { get; private set; }
        public bool IsUsable { get; private set; }
    }
}