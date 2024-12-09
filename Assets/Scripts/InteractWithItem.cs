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
        text.text = "";

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range, layerMask))
        {
            
            //Debug.Log("You hit " + hit.transform.gameObject.name + " in front of you");

            if (hit.transform.CompareTag("Item"))
            {
                grab(hit);
            }


            if (hit.transform.CompareTag("Harvestable"))
            {
                harvest(hit);
            }
        }
    }

    void grab(RaycastHit hit)
    {
        GameObject obj = hit.transform.gameObject;
        // sauvegarde du nom pour le DEBUG
        string name = new string(obj.name);

        if (inventory.HaveSpace())
        {
            text.text = "Appuyez sur E pour ramasser";

            if (Input.GetKeyDown(KeyCode.E))
            {

                inventory.AddItem(obj.GetComponent<Item>().item);
                Destroy(obj);

                Debug.Log("Player tried to grab an item('" + name + "') and it succeded");
            }
        }
        else
        {
            text.text = "Inventaire plein";
            Debug.Log("Player tried to pick an item('" + name + "') but it fails because his inventory is full");
        }
    }



    void harvest(RaycastHit hit)
    {
        GameObject crop = hit.transform.gameObject;

        // sauvegarde du nom pour le DEBUG
        string name = new string(crop.name);

        // récupération du tableau de 'harvestable' pour la collecte des objets
        harvestable = crop.GetComponent<Harvestable>();

        if (inventory.content.Exists(item => Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(item.itemData)) == "Hoe"))
        {
            text.text = "Appuyer sur E pour récolter";

            if (Input.GetKeyDown(KeyCode.E))
            {
                // transformation du plant ('crop') en GameObjects ressource à ramasser
                for (int i = 0; i < harvestable.harvestableItems.Length; i++)
                {
                    Ressource ressource = harvestable.harvestableItems[i];

                    for (int j = 0; j < Random.Range(ressource.minRessource, ressource.maxRessource); j++)
                    {
                        GameObject instantiatedRessource = GameObject.Instantiate(ressource.itemData.prefab);
                        instantiatedRessource.transform.position = harvestable.transform.position;
                    }
                }

                Destroy(crop);

                Debug.Log("Player tried to harvest a crop('" + name + "') and it succeded");

            }
        }
        else
        {
            text.text = "Il vous faut une Houe pour récolter";
            Debug.Log("Player watch over an harvestable but he can't havest it because the specific tool is not in his inventory");
        }
    }

}
