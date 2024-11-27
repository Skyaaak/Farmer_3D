using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "item/New item")]
public class ItemData : ScriptableObject
{
    public string name;
    public string description;
    public ItemType type;
    public Sprite visuel;
    public GameObject prefab;
    public bool stackable;
}


public enum ItemType
{
    Tool,
    Ressource
}