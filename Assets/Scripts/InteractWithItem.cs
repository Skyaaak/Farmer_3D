using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//Fonction permettant l'interaction avec les objets
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

        //On utilse un rayon vers l'avant pour savoir si on regarde un objet avec lequel on peut intéragir
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, layerMask))
        {
            //On regarde le tag de l'objet pour agir en fonction
            if (hit.transform.CompareTag("Item"))
            {
                //Si c'est un item et qu'on à de la place, on donne la possibilité de le ramasser avec E
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
                //Si c'est un harvestable, on regarde si l'objet équipé a pour nom "Hoe"
                //On regarde si c'est un objet de type Hoe : if (Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(inventory.toolEquipped)) == "Hoe")
                if(inventory.toolEquipped?.nameItem == "Hoe")
                {
                    //Si on as la Houe on donne la possibilité de récolter
                    text.text = "Appuyer sur E pour récolter";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Object récolter");

                        harvestable = hit.transform.gameObject.GetComponent<FullGrownItem>();

                        //On boucle sur chaque objet différent que peut dropper le plant
                        for (int i = 0; i < harvestable.harvestableItems.Length; i++)
                        {
                            Ressource ressource = harvestable.harvestableItems[i];

                            //Pour chaque ressource, on génère un nombre aléatoire entre le minimum et le maximum de ressources possible
                            for (int j = 0; j < Random.Range(ressource.minRessource, ressource.maxRessource); j++)
                            {
                                GameObject instantiatedRessource = GameObject.Instantiate(ressource.itemData.prefab);
                                instantiatedRessource.transform.position = harvestable.transform.position;
                            }
                        }

                        //On finit par détruire l'objet
                        Destroy(hit.transform.gameObject);

                    }
                }
                //Si on as pas de Houe on affiche le text nécessaire
                else
                {
                    text.text = "Il vous faut une Houe pour récolter";
                }
            }
            if (hit.transform.CompareTag("CapsuleDirt"))
            {
                //Si c'est une parcelle de terre on regarde si elle est labourée
                Dirt dirtSee = hit.transform.gameObject.GetComponent<Dirt>();
                
                if (!dirtSee.plowed)
                {
                    //Si elle n'est pas labouré on regarde si on as la houe pour donner la possibilité de labourer
                    if (inventory.toolEquipped?.nameItem == "Hoe")
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

                    //Si la terre à été labourré on regarde si des graines ont été plantées
                    if (!harvestableSee.isPlanted)
                    {
                        //Si on as pas déjà de graine plantées on en plante si on as des graines dans l'inventaire

                        if (inventory.toolEquipped?.type == ItemType.Seed)
                        {
                            text.text = "Appuyez sur E pour planter les graines";
                            if (Input.GetKeyDown(KeyCode.E))
                            {

                                harvestableSee.isSeedeed(tomatoSeed);
                            }
                        }
                        else
                        {
                            text.text = "Prenez des graines pour les planter";
                        }
                        
                    }
                    else
                    {
                        //Si on a déjà planté quelque chose on regarde si on peut ramasser
                        if (!harvestableSee.isHarvestable)
                        {
                            text.text = harvestableSee.type + " planté depuis " + (harvestableSee.dayTracker == 0 ? "aujourd'hui" : harvestableSee.dayTracker + (harvestableSee.dayTracker > 1 ? " jours" : " jour"));
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
            //On ne regarde pas d'objet
        }
    }
}
