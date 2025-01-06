using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
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

    [SerializeField]
    public SeedData tomatoSeed;

    [SerializeField]
    public GameController gameController;

    [SerializeField]
    public GameObject menuDeNuit;

    [SerializeField]
    private TextMeshProUGUI textNumeroJour;

    [SerializeField]
    public GameObject inventaire;

    // Update is called once per frame
    void Update()
    {
        text.text = "";

        //On utilse un rayon vers l'avant pour savoir si on regarde un objet avec lequel on peut int�ragir
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, layerMask))
        {
            //On regarde le tag de l'objet pour agir en fonction
            if (hit.transform.CompareTag("Item"))
            {
                //Si c'est un item et qu'on � de la place, on donne la possibilit� de le ramasser avec E
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
                //Si c'est un harvestable, on regarde si l'objet �quip� a pour nom "Hoe"
                //On regarde si c'est un objet de type Hoe : if (Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(inventory.toolEquipped)) == "Hoe")
                if(inventory.toolEquipped?.nameItem == "Hoe")
                {
                    //Si on as la Houe on donne la possibilit� de r�colter
                    text.text = "Appuyer sur E pour r�colter";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Object r�colter");

                        FullGrownItem fullGrownItem = hit.transform.gameObject.GetComponent<FullGrownItem>();
                        Harvestable harvestable = hit.transform.GetComponentInParent<Harvestable>();

                        //On boucle sur chaque objet diff�rent que peut dropper le plant
                        for (int i = 0; i < fullGrownItem.harvestableItems.Length; i++)
                        {
                            Ressource ressource = fullGrownItem.harvestableItems[i];

                            //Pour chaque ressource, on g�n�re un nombre al�atoire entre le minimum et le maximum de ressources possible
                            for (int j = 0; j < Random.Range(ressource.minRessource, ressource.maxRessource); j++)
                            {
                                //On instancie un objet
                                GameObject instantiatedRessource = GameObject.Instantiate(ressource.itemData.prefab);

                                if(harvestable.plantType == PlantType.Plant)
                                {
                                    //On modifie l�g�rement sa position pour qu'il soit ramassable
                                    Vector3 newPos = fullGrownItem.transform.position;
                                    newPos.x += 0.5f;
                                    instantiatedRessource.transform.position = newPos;
                                }
                                else
                                {
                                    Vector3 newPos = fullGrownItem.transform.position;
                                    newPos.y += 0.2f;
                                    instantiatedRessource.transform.position = newPos;
                                }
                                
                            }
                        }

                        harvestable.isPickedUp();
                    }
                }
                //Si on as pas de Houe on affiche le text n�cessaire
                else
                {
                    text.text = "Il vous faut une Houe pour r�colter";
                }
            }
            if (hit.transform.CompareTag("CapsuleDirt"))
            {
                //Si c'est une parcelle de terre on regarde si elle est labour�e
                Dirt dirtSee = hit.transform.gameObject.GetComponent<Dirt>();
                
                if (!dirtSee.plowed)
                {
                    //Si elle n'est pas labour� on regarde si on as la houe pour donner la possibilit� de labourer
                    if (inventory.toolEquipped?.nameItem == "Hoe")
                    {
                        text.text = "Appuyez sur E pour labourr�";
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

                    //Si la terre � �t� labourr� on regarde si des graines ont �t� plant�es
                    if (!harvestableSee.isPlanted)
                    {
                        //Si on as pas d�j� de graine plant�es on en plante si on as des graines dans l'inventaire

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
                        //Si on a d�j� plant� quelque chose on regarde si on peut ramasser
                        if (!harvestableSee.isHarvestable)
                        {
                            text.text = harvestableSee.type + " plant� depuis " + (harvestableSee.dayTracker == 0 ? "aujourd'hui" : harvestableSee.dayTracker + (harvestableSee.dayTracker > 1 ? " jours" : " jour"));
                        }
                        else
                        {
                            text.text = harvestableSee.type + " ramassable";
                        }
                        
                    }
                   
                }
            }
            if (hit.transform.CompareTag("Door"))
            {
                text.text = "Appuyez sur E pour terminer la journ�";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    //On d�sactive l'affichage de l'inventaire et on active l'affichage du menu de nuit
                    inventaire.SetActive(false);
                    menuDeNuit.SetActive(true);
                    //On d�sactive le mouvement de la cam�ra et on r�active la souris
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    //On change le text pour afficher le jour et on lance le nouveau jour
                    textNumeroJour.text = "Fin du jour " + gameController.days;
                    gameController.NewDay();
                }
            }
        }
        else
        {
            //On ne regarde pas d'objet
        }
    }



    void harvest(RaycastHit hit)
    {
        GameObject crop = hit.transform.gameObject;

        // sauvegarde du nom pour le DEBUG
        string name = new string(crop.name);

        // r�cup�ration du tableau de 'harvestable' pour la collecte des objets
        harvestable = crop.GetComponent<Harvestable>();

        if (inventory.content.Exists(item => Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(item.itemData)) == "Hoe"))
        {
            text.text = "Appuyer sur E pour r�colter";

            if (Input.GetKeyDown(KeyCode.E))
            {
                // transformation du plant ('crop') en GameObjects ressource � ramasser
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
            text.text = "Il vous faut une Houe pour r�colter";
            Debug.Log("Player watch over an harvestable but he can't havest it because the specific tool is not in his inventory");
        }
    }

}
