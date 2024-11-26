using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemData item;

    public ItemData GetItemData()
    {
        return item;
    }
}
