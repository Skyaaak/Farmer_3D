using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithItem : MonoBehaviour
{
    [SerializeField]
    private float range = 1.5f;

    public Inventory inventory;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Text text;

    private Harvestable harvestable;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        text.text = "";

        if (Physics.Raycast(transform.position, transform.forward, out hit, range, layerMask))
        {
            //text.SetActive(true);

            if (hit.transform.CompareTag("Item"))
            {
                ItemData itemSee = hit.transform.gameObject.GetComponent<Item>().item;

                text.text = inventory.HaveSpace(itemSee) ? "Appuyez sur E pour ramasser" : "Inventaire plein";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (inventory.HaveSpace(itemSee))
                    {
                        inventory.AddItem(hit.transform.gameObject.GetComponent<Item>().item);
                        Destroy(hit.transform.gameObject);
                    }
                    else
                    {
                        Debug.Log("Inventaire plein");
                    }

                }
            }
            if (hit.transform.CompareTag("Harvestable"))
            {

                if (inventory.content.Exists(item => Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(item.itemData) )== "Hoe"))
                {
                    text.text = "Appuyer sur E pour récolter";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Object récolter");

                        harvestable = hit.transform.gameObject.GetComponent<Harvestable>();

                        for (int i = 0; i < harvestable.harvestableItems.Length; i++)
                        {
                            Ressource ressource = harvestable.harvestableItems[i];

                            for (int j = 0; j < Random.Range(ressource.minRessource, ressource.maxRessource); j++)
                            {
                                GameObject instantiatedRessource = GameObject.Instantiate(ressource.itemData.prefab);
                                instantiatedRessource.transform.position = harvestable.transform.position;
                            }
                        }

                        Destroy(hit.transform.gameObject);

                    }
                }
                else
                {
                    text.text = "Il vous faut une Houe pour récolter";
                }
            }
        }
        else
        {
            //text.SetActive(false);
        }
    }
}
