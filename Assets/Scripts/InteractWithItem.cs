using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithItem : MonoBehaviour
{
    /*[SerializeField]
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
                ItemData itemSee = hit.transform.gameObject.GetComponent<ItemData>().item;

                text.text = inventory.HaveSpace(itemSee) ? "Appuyez sur E pour ramasser" : "Inventaire plein";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (inventory.HaveSpace(itemSee))
                    {
                        inventory.AddItem(hit.transform.gameObject.GetComponent<ItemData>().item);
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

                if ( Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(inventory.toolEquipped) )== "Hoe")
                {
                    text.text = "Appuyer sur E pour r�colter";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Object r�colter");

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
                    text.text = "Il vous faut une Houe pour r�colter";
                }
            }
        }
        else
        {
            //text.SetActive(false);
        }
    }*/
}
