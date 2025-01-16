using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

//Fonction permettant l'interaction avec les objets
public class InteractWithItem : MonoBehaviour
{
    [SerializeField]
    private float range = 1.5f;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Text text;

    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private GameObject menuDeNuit;

    [SerializeField]
    private TextMeshProUGUI textNumeroJour;

    [SerializeField]
    private TextMeshProUGUI textMoneyJour;

    [SerializeField]
    private GameObject inventaire;

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

                if (inventory.HaveSpace(itemSee))
                {
                    var endText = "";
                    switch (itemSee.type)
                    {
                        case ItemType.Ressource: endText = "la " + itemSee.nameItem; break;
                        case ItemType.Tool: endText = "la " + itemSee.nameItem; break;
                        case ItemType.Seed: endText = "le sac de graines de " + itemSee.seed.typeOfSeed; break;
                        case ItemType.Sappling: endText = "la pousse de " + itemSee.sappling.getTypeOfSappling(); break;
                    }

                    text.text = "Appuyez sur E pour ramasser " + endText;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        inventory.AddItem(hit.transform.gameObject.GetComponent<Item>().item);
                        Destroy(hit.transform.gameObject);
                    }
                }
                else
                {
                    text.text = "Inventaire plein";
                }
                
            }
            if (hit.transform.CompareTag("Harvestable"))
            {
                FullGrownItem fullGrownItem = hit.transform.gameObject.GetComponent<FullGrownItem>();

                //Si c'est un harvestable, on regarde si il � besoin d'un objet pour �tre ramass� et si, le cas pr�sent, l'objet n�cessaire est l'objet �quip�
                if (fullGrownItem.GetToolRequired() == null || inventory.toolEquipped == fullGrownItem.GetToolRequired())
                {
                    //Si on as la Faucille on donne la possibilit� de r�colter
                    text.text = "Appuyer sur E pour r�colter";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Harvestable harvestable = hit.transform.GetComponentInParent<Harvestable>();

                        //On boucle sur chaque objet différent que peut dropper le plant
                        for (int i = 0; i < fullGrownItem.harvestableItems.Length; i++)
                        {
                            Ressource ressource = fullGrownItem.harvestableItems[i];

                            //Pour chaque ressource, on génère un nombre aléatoire entre le minimum et le maximum de ressources possible
                            for (int j = 0; j < Random.Range(ressource.minRessource, ressource.maxRessource); j++)
                            {
                                //On instancie un objet
                                GameObject instantiatedRessource = GameObject.Instantiate(ressource.itemData.prefab);

                                if (harvestable.plantType == PlantType.Plant)
                                {
                                    //On modifie l�g�rement sa position pour qu'il soit ramassable
                                    Vector3 newPos = harvestable.transform.position;
                                    print("avant changement: " + newPos);
                                    newPos.z += ressource.itemData.prefab.transform.position.z;
                                    newPos.y += ressource.itemData.prefab.transform.position.y;
                                    newPos.x += 0.5f;
                                    print("apr�s changement: " + newPos);
                                    instantiatedRessource.transform.position = newPos;
                                }
                                else
                                {
                                    Vector3 newPos = harvestable.transform.position;
                                    newPos.y += 0.2f;
                                    instantiatedRessource.transform.position = newPos;
                                }

                            }
                        }

                        harvestable.isPickedUp();
                    }
                }
                //Si on as pas l'objet ad�quat on affiche le text n�cessaire
                else
                {
                    text.text = "Il vous faut l'outil "+ fullGrownItem.GetToolRequired().nameItem + " pour r�colter";
                }
            }
            if (hit.transform.CompareTag("CapsuleDirt"))
            {
                //Si c'est une parcelle de terre on regarde si elle est labourée
                Dirt dirtSee = hit.transform.gameObject.GetComponent<Dirt>();

                if (!dirtSee.plowed)
                {
                    //Si elle n'est pas labour� on regarde si on as la houe pour donner la possibilit� de labourer
                    if (inventory.toolEquipped?.nameItem == "Houe")
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
                                SeedData seed = inventory.toolEquipped.seed;
                                harvestableSee.isSeedeed(seed);
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
            if (hit.transform.CompareTag("Door"))
            {
                text.text = "Appuyez sur E pour terminer la journé";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    //On désactive l'affichage de l'inventaire et on active l'affichage du menu de nuit
                    inventaire.SetActive(false);
                    menuDeNuit.SetActive(true);
                    //On désactive le mouvement de la caméra et on réactive la souris
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    //On change le text pour afficher le jour et on lance le nouveau jour
                    textNumeroJour.text = "Fin du jour " + gameController.days;
                    textMoneyJour.text = "Argent gagn� pendant la journ�e: " + gameController.moneyWin;
                    gameController.NewDay();
                }
            }
            if (hit.transform.CompareTag("Pickable"))
            {
                //Si on as la Houe on donne la possibilit� de r�colter
                text.text = "Appuyer sur E pour ceuillir les fruits";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    FullGrownItem fullGrownItem = hit.transform.gameObject.GetComponent<FullGrownItem>();
                    TreeLand treeLand = hit.transform.GetComponentInParent<TreeLand>();

                    //On boucle sur chaque objet diff�rent que peut dropper l'arbre
                    for (int i = 0; i < fullGrownItem.harvestableItems.Length; i++)
                    {
                        Ressource ressource = fullGrownItem.harvestableItems[i];

                        //Pour chaque ressource, on g�n�re un nombre al�atoire entre le minimum et le maximum de ressources possible
                        for (int j = 0; j < Random.Range(ressource.minRessource, ressource.maxRessource); j++)
                        {
                            //On instancie un objet
                            GameObject instantiatedRessource = GameObject.Instantiate(ressource.itemData.prefab);
                            float xRand = (float) Random.Range(4, 8)/10;
                            int xSigne = Random.Range(0, 2);
                            float zRand = (float) Random.Range(4, 8)/10;
                            int zSigne = Random.Range(0, 2);
                            Vector3 newPos = fullGrownItem.transform.position;
                            if(xSigne == 0)
                            {
                                newPos.x += xRand;
                            }
                            else
                            {
                                newPos.x -= xRand;
                            }

                            if (zSigne == 0)
                            {
                                newPos.z += zRand;
                            }
                            else
                            {
                                newPos.z -= zRand;
                            }
                            instantiatedRessource.transform.position = newPos;
                        }
                    }

                    treeLand.PickUp();
                }
            }
            if (hit.transform.CompareTag("TreeLand"))
            {
                TreeLand treeLand = hit.transform.gameObject.GetComponent<TreeLand>();
                if (treeLand.isPlanted())
                {
                    if (!treeLand.isPickable())
                    {
                        text.text = treeLand.getTreeName() + " plant� depuis " + (treeLand.daySincePlantation() == 0 ? "aujourd'hui" : treeLand.daySincePlantation() + (treeLand.daySincePlantation() > 1 ? " jours" : " jour"));
                    }
                    else
                    {
                        text.text = treeLand.getTreeName() + " ramassable";
                    }
                }
                else
                {
                    if (inventory.toolEquipped?.type == ItemType.Sappling)
                    {
                        text.text = "Appuyez sur E pour planter l'arbre";
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            SapplingData sappling = inventory.toolEquipped.sappling;
                            treeLand.Plant(sappling);
                        }
                    }
                    else
                    {
                        text.text = "Equipez une pousse d'arbre pour planter";
                    }
                }
            }
        }
        else
        {
            //On ne regarde pas d'objet
        }

        // Appuyez sur R pour lâcher l'outil équipé
        if (Input.GetKeyDown(KeyCode.R))
        {
            DropEquippedTool();
        }
    }


    private void DropEquippedTool()
    {
        // Vérifie si un outil est équipé
        if (inventory.toolEquipped != null)
        {
            // Instancie le prefab de l'outil
            GameObject droppedTool = Instantiate(inventory.toolEquipped.prefab);

            // Positionne l'objet juste devant le joueur
            Vector3 dropPosition = transform.position + transform.forward * 1.0f; // Position devant le joueur
            dropPosition.y = transform.position.y + 1.0f; // Légèrement au-dessus du sol
            droppedTool.transform.position = dropPosition;

            // Ajoute un Rigidbody pour que l'objet tombe au sol
            if (droppedTool.GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = droppedTool.AddComponent<Rigidbody>();
                rb.mass = 1.0f; // Vous pouvez ajuster la masse si nécessaire
                rb.isKinematic = false;
            } 
            else
            {
                Rigidbody rb = droppedTool.GetComponent<Rigidbody>();
                rb.mass = 1.0f; // Vous pouvez ajuster la masse si nécessaire
                rb.isKinematic = false;
            }

            // Retire l'outil de l'inventaire
            inventory.toolEquipped = null;
            inventory.RefreshContent();

            // Optionnel : Affiche un message pour confirmer
            Debug.Log("Outil lâché : " + droppedTool.name);
        }
        else
        {
            Debug.Log("Aucun outil équipé à lâcher.");
        }
    }
}
