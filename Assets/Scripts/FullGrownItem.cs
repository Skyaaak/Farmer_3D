using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullGrownItem : MonoBehaviour
{
    //Variable contenant chaque Item pouvant �tre dropp� par le plant ainsi que leurs quantit� minimum et maximum
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