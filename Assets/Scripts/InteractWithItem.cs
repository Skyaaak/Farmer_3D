using System.Collections;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithItem : MonoBehaviour
{
    [SerializeField] private float range = 1.5f;
    [SerializeField] private Inventory inventory;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Text text;

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
                text.text = inventory.HaveSpace() ? "Appuyez sur E pour ramasser" : "Inventaire plein";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (inventory.HaveSpace())
                    {
                        inventory.AddItem(hit.transform.gameObject.GetComponent<Item>());
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
                if (inventory.GetContent().Exists(item => Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(item.getItemData()) )== "Hoe"))
                {
                    text.text = "Appuyer sur E pour r�colter";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Object r�colter");

                        harvestable = hit.transform.gameObject.GetComponent<Harvestable>();

                        for (int i = 0; i < harvestable.harvest().Length; i++)
                        {
                            Resource ressource = harvestable.harvest()[i];

                            for (int j = 0; j < Random.Range(ressource.GetMinResource(), ressource.GetMaxResource()); j++)
                            {
                                GameObject instantiatedRessource = GameObject.Instantiate(ressource.GetItemData().prefab);
                                instantiatedRessource.transform.position = harvestable.getHarvestablePosition();
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
    }
}
