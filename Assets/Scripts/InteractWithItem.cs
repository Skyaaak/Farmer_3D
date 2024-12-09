using System.Collections;
using System.Collections.Generic;
using System.IO;
using InventoryManager;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithItem : MonoBehaviour
{
    [SerializeField] private float range = 1.5f;

    [SerializeField] private Inventory inventory;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Text text;

    private Item harvestable;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        text.text = "";

        hit = launchRayCast();

        CheckOnRaycastHit(hit);
    }

    private RaycastHit launchRayCast()
    {
        RaycastHit raycastHit;
        Physics.Raycast(transform.position, transform.forward, out raycastHit, range, layerMask);
        Debug.Log(raycastHit);

        return raycastHit;
    }

    private void CheckOnRaycastHit(RaycastHit raycastHit)
    {
        if (raycastHit.collider == null)
        {
            return;
        }
        
        RaycastHitItem(raycastHit);
        RaycastHitHarvestable(raycastHit);
    }

    private void RaycastHitItem(RaycastHit raycastHit)
    {
        if (raycastHit.transform.CompareTag("Item"))
        {
            Item itemSee = raycastHit.transform.gameObject.GetComponent<Item>();

            text.text = inventory.HasSpace(itemSee) ? "Appuyez sur E pour ramasser" : "Inventaire plein";

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (inventory.HasSpace(itemSee))
                {
                    inventory.AddItem(raycastHit.transform.gameObject.GetComponent<Item>(), 1);
                    Destroy(raycastHit.transform.gameObject);
                }
                else
                {
                    Debug.Log("Inventaire plein");
                }

            }
        }
    }

    private void RaycastHitHarvestable(RaycastHit raycastHit)
    {
        /*if (raycastHit.transform.CompareTag("Harvestable"))
        {

            if (Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(inventory.toolEquipped)) == "Hoe")
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

                    Destroy(raycastHit.transform.gameObject);

                }
            }
            else
            {
                text.text = "Il vous faut une Houe pour récolter";
            }
        }*/
    }
}
