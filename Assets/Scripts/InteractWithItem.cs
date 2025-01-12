using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    private UnityEngine.UI.Button endButton;

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
                    text.text = LanguageManager.Instance.GetTranslation("pressToPickUp") + LanguageManager.Instance.GetTranslation(itemSee.nameItem.ToLower() + "Gender"); ;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        inventory.AddItem(hit.transform.gameObject.GetComponent<Item>().item);
                        Destroy(hit.transform.gameObject);
                    }
                }
                else
                {
                    text.text = LanguageManager.Instance.GetTranslation("inventoryFull");
                }
                
            }
            if (hit.transform.CompareTag("Harvestable"))
            {
                FullGrownItem fullGrownItem = hit.transform.gameObject.GetComponent<FullGrownItem>();

                //Si c'est un harvestable, on regarde si il à besoin d'un objet pour être ramassé et si, le cas présent, l'objet nécessaire est l'objet équipé
                if (fullGrownItem.GetToolRequired() == null || inventory.toolEquipped == fullGrownItem.GetToolRequired())
                {
                    //Si on as la Faucille on donne la possibilité de récolter
                    text.text = LanguageManager.Instance.GetTranslation("pressToHarvest");
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

                                if(harvestable.plantType == PlantType.Plant)
                                {
                                    //On modifie légérement sa position pour qu'il soit ramassable
                                    Vector3 newPos = harvestable.transform.position;
                                    newPos.z += ressource.itemData.prefab.transform.position.z;
                                    newPos.y += ressource.itemData.prefab.transform.position.y;
                                    newPos.x += 0.5f;
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
                //Si on as pas l'objet adéquat on affiche le text nécessaire
                else
                {
                    text.text = LanguageManager.Instance.GetTranslation("needTool") + fullGrownItem.GetToolRequired().nameItem + LanguageManager.Instance.GetTranslation("toHarvest");
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
                        text.text = LanguageManager.Instance.GetTranslation("pressToPlow");
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            dirtSee.isGettingPlowed();
                        }
                    }
                    else
                    {
                        text.text = LanguageManager.Instance.GetTranslation("toolToPlow");
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
                            text.text = LanguageManager.Instance.GetTranslation("pressToSeed");
                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                SeedData seed = inventory.toolEquipped.seed;
                                harvestableSee.isSeedeed(seed);
                            }
                        }
                        else
                        {
                            text.text = LanguageManager.Instance.GetTranslation("seedToSeed");
                        }
                        
                    }
                    else
                    {
                        //Si on a déjà planté quelque chose on regarde si on peut ramasser
                        if (!harvestableSee.isHarvestable)
                        {
                            text.text = harvestableSee.type + LanguageManager.Instance.GetTranslation("plantSince") + (harvestableSee.dayTracker == 0 ? LanguageManager.Instance.GetTranslation("today") : harvestableSee.dayTracker + (harvestableSee.dayTracker > 1 ? LanguageManager.Instance.GetTranslation("days") : LanguageManager.Instance.GetTranslation("day")));
                        }
                        else
                        {
                            text.text = harvestableSee.type + LanguageManager.Instance.GetTranslation("harvestable");
                        }
                        
                    }
                   
                }
            }
            if (hit.transform.CompareTag("Door"))
            {
                text.text = LanguageManager.Instance.GetTranslation("pressToSleep");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    //On désactive l'affichage de l'inventaire et on active l'affichage du menu de nuit
                    inventaire.SetActive(false);
                    menuDeNuit.SetActive(true);
                    //On désactive le mouvement de la caméra et on réactive la souris
                    Time.timeScale = 0;
                    UnityEngine.Cursor.visible = true;
                    UnityEngine.Cursor.lockState = CursorLockMode.None;
                    //On change le text pour afficher le jour et on lance le nouveau jour
                    textNumeroJour.text = LanguageManager.Instance.GetTranslation("endDay") + gameController.days;
                    textMoneyJour.text = LanguageManager.Instance.GetTranslation("moneyWin") + gameController.moneyWin;
                    var textendButton = endButton.GetComponentInChildren<TextMeshProUGUI>();
                    textendButton.text = LanguageManager.Instance.GetTranslation("endRecap");
                    gameController.NewDay();
                }
            }
            if (hit.transform.CompareTag("Pickable"))
            {
                //Si on as la Houe on donne la possibilité de récolter
                text.text = LanguageManager.Instance.GetTranslation("pressToShake");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    FullGrownItem fullGrownItem = hit.transform.gameObject.GetComponent<FullGrownItem>();
                    TreeLand treeLand = hit.transform.GetComponentInParent<TreeLand>();

                    //On boucle sur chaque objet différent que peut dropper l'arbre
                    for (int i = 0; i < fullGrownItem.harvestableItems.Length; i++)
                    {
                        Ressource ressource = fullGrownItem.harvestableItems[i];

                        //Pour chaque ressource, on génère un nombre aléatoire entre le minimum et le maximum de ressources possible
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
                        text.text = treeLand.getTreeName() + LanguageManager.Instance.GetTranslation("plantSince") + (treeLand.daySincePlantation() == 0 ? LanguageManager.Instance.GetTranslation("today") : treeLand.daySincePlantation() + (treeLand.daySincePlantation() > 1 ? LanguageManager.Instance.GetTranslation("days") : LanguageManager.Instance.GetTranslation("day")));
                    }
                    else
                    {
                        text.text = treeLand.getTreeName() + LanguageManager.Instance.GetTranslation("harvestable");
                    }
                }
                else
                {
                    if (inventory.toolEquipped?.type == ItemType.Sappling)
                    {
                        text.text = LanguageManager.Instance.GetTranslation("pressToPlant");
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            SapplingData sappling = inventory.toolEquipped.sappling;
                            treeLand.Plant(sappling);
                        }
                    }
                    else
                    {
                        text.text = LanguageManager.Instance.GetTranslation("takeSappling");
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
