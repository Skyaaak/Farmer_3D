using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullGrownItem : MonoBehaviour
{
    //Variable contenant chaque Item pouvant être droppé par le plant ainsi que leurs quantité minimum et maximum
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