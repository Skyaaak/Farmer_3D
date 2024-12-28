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

    private FullGrownItem harvestable;

    [SerializeField]
    public SeedData tomatoSeed;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        text.text = "";

        if (Physics.Raycast(transform.position, transform.forward, out hit, range, layerMask))
        {

            if (hit.transform.CompareTag("Item"))
            {
                ItemData itemSee = hit.transform.gameObject.GetComponent<Item>().item;

                bool haveSpace = inventory.HaveSpace(itemSee);

                text.text = haveSpace ? "Appuyez sur E pour ramasser" : "Inventaire plein";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (haveSpace)
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

                if (Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(inventory.toolEquipped)) == "Hoe")
                {
                    text.text = "Appuyer sur E pour récolter";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Object récolter");

                        harvestable = hit.transform.gameObject.GetComponent<FullGrownItem>();

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
            if (hit.transform.CompareTag("CapsuleDirt"))
            {
                Dirt dirtSee = hit.transform.gameObject.GetComponent<Dirt>();
                if (!dirtSee.plowed)
                {
                    if (Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(inventory.toolEquipped)) == "Hoe")
                    {
                        text.text = "Appuyez sur E pour labourré";
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            dirtSee.isGettingPlowed();
                        }
                    }
                    else
                    {
                        text.text = "Prenez la houe pour labourrer";
                    }
                }
                else
                {
                    HarvestableInstance harvestableSee = hit.transform.gameObject.GetComponent<HarvestableInstance>();

                    if (!harvestableSee.isPlanted)
                    {
                        text.text = "Appuyez sur E pour planter les graines";
                        if (Input.GetKeyDown(KeyCode.E))
                        {

                            harvestableSee.isSeedeed(tomatoSeed);
                        }
                    }
                    else
                    {
                        if (!harvestableSee.isHarvestable)
                        {
                            text.text = harvestableSee.type + " planté depuis " + harvestableSee.dayTracker + (harvestableSee.dayTracker == 0 ? "aujourd'hui" : harvestableSee.dayTracker > 1 ? " jours" : " jour");
                        }
                        else
                        {
                            text.text = harvestableSee.type + " ramassable";
                        }
                        
                    }
                   
                }
            }
        }
        else
        {
            //text.SetActive(false);
        }
    }
}
