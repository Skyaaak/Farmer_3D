using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public List<ItemInInventory> content = new List<ItemInInventory>();

    [SerializeField]
    public GameObject inventoryPanel;

    public ItemData toolEquipped;

    [SerializeField] private ToolSlot toolSlot;

    [SerializeField]
    private Transform inventorySlotsParent;

    const int maxSize = 5;
    const int maxWeight = 10000;
    public int actualWeight = 0;

    public Sprite emptySlotVisual;

    public void Start()
    {
        RefreshContent();
    }

    public void AddItem(ItemData item)
    {
        if(item.type == ItemType.Ressource)
        {
            ItemInInventory itemInInventory = content.Where(element => element.itemData == item).FirstOrDefault();

            if (itemInInventory != null && item.stackable)
            {
                itemInInventory.count++;
                actualWeight += item.weight;
            }
            else
            {
                content.Add(new ItemInInventory { itemData = item, count = 1 });
                actualWeight += item.weight;
            }
            Debug.Log("actualWeight= " + actualWeight);
        }
        else
        {
            toolEquipped = item;
        }
        
        RefreshContent();
        
    }

    public void RemoveItem(ItemData item)
    {
        ItemInInventory itemInInventory = content.Where(element => element.itemData == item).FirstOrDefault();

        if (itemInInventory.count > 1)
        {
            itemInInventory.count--;
        }
        else
        {
            content.Remove(itemInInventory);
        }

        RefreshContent();
    }

    public List<ItemInInventory> GetContent()
    {
        return content;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    private void RefreshContent()
    {

        for (int i = 0; i < inventorySlotsParent.childCount; i++)
        {
            Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();

            currentSlot.item = null;
            currentSlot.itemVisual.sprite = emptySlotVisual;
            currentSlot.countText.enabled = false;
        }

        for (int i = 0; i < content.Count; i++)
        {
            Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();

            currentSlot.item = content[i].itemData;
            currentSlot.itemVisual.sprite = content[i].itemData.visuel;

            if (currentSlot.item.stackable)
            {
                currentSlot.countText.enabled = true;
                currentSlot.countText.text = content[i].count.ToString();
            }
            //inventorySlotsParent.GetChild(i).GetChild(0).GetComponent<Image>().sprite = content[i].itemData.visuel;
        }

        if (toolEquipped)
        {
            toolSlot.item = toolEquipped;
            toolSlot.itemVisual.sprite = toolEquipped.visuel;
        }
    }

    public bool HaveSpace(ItemData item)
    {
        if(item.type == ItemType.Ressource)
        {
            ItemInInventory itemInInventory = content.Where(element => element.itemData == item).FirstOrDefault();

            if (itemInInventory != null && item.stackable)
            {
                if (actualWeight + item.weight <= maxWeight)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (actualWeight + item.weight <= maxWeight)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            if (actualWeight + item.weight <= maxWeight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}

[System.Serializable]
public class ItemInInventory
{
    public ItemData itemData;
    public int count;
}
