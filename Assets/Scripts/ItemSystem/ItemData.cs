using System.Collections;
using System.Collections.Generic;
using ItemSystem;
using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "item", menuName = "item/New item")]
    public class ItemData : ScriptableObject
    {
        public string Name { get; private set; }
        public string description { get; private set; }
        public ItemType type { get; private set; }
    }
}
