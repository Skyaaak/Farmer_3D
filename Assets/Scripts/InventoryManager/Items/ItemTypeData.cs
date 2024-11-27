using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryManager
{

    [CreateAssetMenu(fileName = "itemType", menuName = "item/New item")]
    public class ItemTypeData : ScriptableObject
    {
        [SerializeField] private string itemTypeName;
        [SerializeField] private string description;
        [SerializeField] private Sprite visuel;
        
        public string ItemTypeName => itemTypeName;
        public string Description => description;
        public Sprite Visuel => visuel;
    }
}