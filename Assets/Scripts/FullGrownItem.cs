using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullGrownItem : MonoBehaviour
{
    [SerializeField]
    public Ressource[] harvestableItems;
}

[System.Serializable]
public class Ressource
{
    public ItemData itemData;
    public int minRessource;
    public int maxRessource;

}