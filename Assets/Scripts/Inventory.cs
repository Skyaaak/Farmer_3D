using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using DefaultNamespace;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemInInventory> content = new List<ItemInInventory>();
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform inventorySlotsParent;

    const int maxSize = 3;
    [SerializeField]  private Sprite emptySlotVisual;

    public void Start()
    {
        RefreshContent();
    }

    public void AddItem(Item item)
    {
        ItemInInventory itemInInventory = content.Where(element => element.getItemData() == item).FirstOrDefault();

        if (itemInInventory != null && item.GetItemData().stackable)
        {
            itemInInventory.addItem();
        }
        else
        {
            content.Add(new ItemInInventory(item.GetItemData(), 1));
        }

        RefreshContent();
    }

    public void RemoveItem(ItemData item)
    {
        ItemInInventory itemInInventory = content.Where(element => element.getItemData() == item).FirstOrDefault();

        if (itemInInventory.getCount() > 1)
        {
            itemInInventory.removeItem();
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

            currentSlot.item = content[i].getItemData();
            currentSlot.itemVisual.sprite = content[i].getItemData().visuel;

            if (currentSlot.item.stackable)
            {
                currentSlot.countText.enabled = true;
                currentSlot.countText.text = content[i].getCount().ToString();
            }
            //inventorySlotsParent.GetChild(i).GetChild(0).GetComponent<Image>().sprite = content[i].itemData.visuel;
        }
    }

    public bool HaveSpace()
    {
        return maxSize != content.Count;
    }
}
