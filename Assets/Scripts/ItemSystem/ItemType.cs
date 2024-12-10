using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "itemType", menuName = "ItemSystem/types/New type")]
    public class ItemType: ScriptableObject
    {
        public string TypeName { get; set; }
        public Sprite TypeIcon { get; set; }
        
        public bool CanUse { get; set; }
        public bool CanStock { get; set; }
        public bool CanEquip { get; set; }
    }
}