using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public List<ItemInInventory> content = new List<ItemInInventory>();

    [SerializeField]
    public GameObject inventoryPanel;

    [SerializeField]
    private Transform inventorySlotsParent;

    const int maxSize = 3;

    public Sprite emptySlotVisual;

    public void Start()
    {
        RefreshContent();
    }

    public void AddItem(ItemData item)
    {
        ItemInInventory itemInInventory = content.Where(element => element.itemData == item).FirstOrDefault();

        if (itemInInventory != null && item.stackable)
        {
            itemInInventory.count++;
        }
        else
        {
            content.Add(new ItemInInventory{itemData = item,count = 1});
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
    }

    public bool HaveSpace()
    {
        return maxSize != content.Count;
    }
}

[System.Serializable]
public class ItemInInventory
{
    public ItemData itemData;
    public int count;
}
