using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class FullGrownItem : MonoBehaviour
{
    //Variable contenant chaque Item pouvant être droppé par le plant ainsi que leurs quantité minimum et maximum
    [SerializeField]
    public Ressource[] harvestableItems;
    [SerializeField]
    private ItemData tool;

    private void OnValidate()
    {
        if (tool != null && tool.type != ItemType.Tool)
        {
            Debug.LogWarning("Cet objet n'est pas de type 'Tool' !", this);
            tool = null;
        }
    }

    public ItemData GetToolRequired()
    {
        return tool;
    }
}

[System.Serializable]
public class Ressource
{
    public ItemData itemData;
    public int minRessource;
    public int maxRessource;

}