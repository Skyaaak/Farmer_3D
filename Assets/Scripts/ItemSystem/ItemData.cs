using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "item", menuName = "ItemSystem/Items/New item")]
    public class ItemData : ScriptableObject
    {
        public string nameItem;
        public string description;
        public ItemType type;
    }
}