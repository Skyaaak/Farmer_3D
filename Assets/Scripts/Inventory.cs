using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

//Inventaire du joueur séparé en deux partis:
//La partie inventaire unique pour un outil ou un sac de graines
//La partie inventaire classique pour les récoltes
public class Inventory : MonoBehaviour , ISaveable
{
    [SerializeField]
    public List<ItemInInventory> content;

    [SerializeField]
    public GameObject inventoryPanel;

    public ItemData toolEquipped;

    [SerializeField]
    private ToolSlot toolSlot;

    [SerializeField]
    private Transform inventorySlotsParent;

    [SerializeField]
    private TextMeshProUGUI moneyText;

    const int maxSize = 5;
    const int maxWeight = 10000;
    public int actualWeight = 0;

    public Sprite emptySlotVisual;

    public void Start()
    {
        SaveInventoryManager.LoadJsonData(new List<ISaveable> { this });
        RefreshContent();
    }

    //Fonction pour l'ajout d'un objet à l'inventaire tel qu'il soit
    public void AddItem(ItemData item)
    {
        //Si l'objet est une ressource on l'ajoute à l'inventaire normal
        if(item.type == ItemType.Ressource)
        {
            //On cherche si cet objet est déjà présent dans l'inventaire
            ItemInInventory itemInInventory = content.Where(element => element.itemData == item).FirstOrDefault();

            //Si l'objet est présent et qu'il est stackable on incrémente le nombre d'objet et on met à jour le poids
            if (itemInInventory != null && item.stackable)
            {
                itemInInventory.count++;
                actualWeight += item.weight;
            }
            //Sinon, on l'ajoute dans un nouvel espace
            else
            {
                content.Add(new ItemInInventory { itemData = item, count = 1 });
                actualWeight += item.weight;
            }
        }
        //SInon on ajoute l'objet dans l'inventaire de l'outil
        else
        {
            toolEquipped = item;
        }
        
        //On rafraichît le visuel de l'inventaire.
        RefreshContent();
        
    }

    //Fonction pour la suppression d'un objet à l'inventaire
    public void RemoveItem(ItemData item)
    {
        if (item.type == ItemType.Ressource)
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
        }
        else
        {
            toolEquipped = null;
        }

        //On rafraichît le visuel de l'inventaire.
        RefreshContent();
    }

    //Fonction permettant de récupérer le contenu de l'inventaire
    public List<ItemInInventory> GetContent()
    {
        return content;
    }

    //Fonction permettant la mise à jour à chaque Frame
    public void Update()
    {
        //Si on appuie sur la touche I on affiche ou cache la barre d'inventaire
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    //Fonction permettant de mettre à jour le visuel de l'inventaire
    public void RefreshContent()
    {
        //On boucle sur chaque slot de l'inventaire et on remet l'affichage par défaut
        for (int i = 0; i < inventorySlotsParent.childCount; i++)
        {
            Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();

            currentSlot.item = null;
            currentSlot.itemVisual.sprite = emptySlotVisual;
            currentSlot.countText.enabled = false;
        }
        //On met l'affichage par défaut sur l'inventaire d'outil 
        toolSlot.item = null;
        toolSlot.itemVisual.sprite = emptySlotVisual;

        //On boucle sur le contenu de l'inventaire
        for (int i = 0; i < content.Count; i++)
        {
            Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();

            if (content[i] != null)
            {
                //On ajoute les données nécessaire à l'affichage
                currentSlot.item = content[i].itemData;
                currentSlot.itemVisual.sprite = content[i].itemData.visuel;

                //Si l'objet est stackable on affiche également le compteur
                if (currentSlot.item.stackable)
                {
                    currentSlot.countText.enabled = true;
                    currentSlot.countText.text = content[i].count.ToString();
                }
                //inventorySlotsParent.GetChild(i).GetChild(0).GetComponent<Image>().sprite = content[i].itemData.visuel;
            }
        }

        //Si on as un outil équipé, on change l'affichage
        if (toolEquipped)
        {
            toolSlot.item = toolEquipped;
            toolSlot.itemVisual.sprite = toolEquipped.visuel;
        }
    }

    //Fonction permettant de vérifier si on as de la place dans l'inventaire
    public bool HaveSpace(ItemData item)
    {
        //Si c'est une ressource, on regarde dans l'inventaire normal
        if(item.type == ItemType.Ressource)
        {
            ItemInInventory itemInInventory = content.Where(element => element.itemData == item).FirstOrDefault();

            //Si l'objet est présnet et qu'il est stackable
            if (itemInInventory != null && item.stackable)
            {
                //On regarde si le futur poids n'est pas au dessus de la capacité du joueur
                if (actualWeight + item.weight <= maxWeight)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //S'il n'est pas présent on regarde s'il à assez de poids disponible et assez de slots disponible
            else
            {
                if (actualWeight + item.weight <= maxWeight && content.Count+1 < maxSize)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //Sinon on regarde l'inventaire d'équippement
        else
        {
            //Si on as pas déjà d'outil et que le joueur peut le porter on retourne true
            if (!toolEquipped && actualWeight + item.weight <= maxWeight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }

    public void PopulateInventory(Inventory a_SaveData)
    {
        a_SaveData.content = new List<ItemInInventory>(this.content);
        a_SaveData.toolEquipped = this.toolEquipped;
        a_SaveData.actualWeight = this.actualWeight;
    }

    public void LoadFromInventory(Inventory a_SaveData)
    {
        this.content = new List<ItemInInventory>(a_SaveData.content);
        this.toolEquipped = a_SaveData.toolEquipped;
        this.actualWeight = a_SaveData.actualWeight;
    }

    public void Sell()
    {
        MainManager.Instance.AddMoney(GetSellAmount());
        moneyText.text = MainManager.Instance.GetMoney().ToString();
        this.content.Clear();
        RefreshContent();
    }

    public int GetSellAmount()
    {
        int price = 0;
        foreach (ItemInInventory itemInInventory in this.content)
        {
            price += itemInInventory.itemData.price * itemInInventory.count;
        }

        return price;
    }
}

//Objet contenant l'item et le nombre d'item stocké
[System.Serializable]
public class ItemInInventory
{
    public ItemData itemData;
    public int count;
}

public interface ISaveable
{
    void PopulateInventory(Inventory a_SaveData);
    void LoadFromInventory(Inventory a_SaveData);
}