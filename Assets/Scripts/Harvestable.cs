using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    public GameObject[] statesOfGrowth;
    public string type;
    public int dayBeforeGrowth;
    public int state;
    public int dayTracker = 0;
    public GameObject actualPrefab;
    public bool isPlanted = false;
    public bool isHarvestable = false;
    public PlantType plantType;
    [SerializeField]
    private Dirt dirt;

    // Update is called once per frame
    void Update()
    {
        
    }

    //Fonction pour ajouter un jour � la plantation
    public void AddDay()
    {
        //On regarde si quelque chose est plant�
        if (isPlanted)
        {
            dayTracker++;
            //On regarrde si le nombre de jour modulo le temps entre deux �tape de pousse est �gale � 0 pour chager le mod�le
            if (dayTracker % dayBeforeGrowth == 0)
            {

                if (state == 0)
                {
                    //On initialise le mod�le correspondant � l'�tape actuelle
                    actualPrefab = Instantiate(statesOfGrowth[state], gameObject.transform);
                    state++;
                }
                else
                {
                    //Si la plantation n'est pas arriv� � terme on la fait avanc�e
                    if (state < statesOfGrowth.Length)
                    {
                        //Si on augmente la culture on d�truit le model actuel avant de mettre le nouveau
                        Destroy(actualPrefab);
                        actualPrefab = Instantiate(statesOfGrowth[state], gameObject.transform);
                        state++;
                    }
                    if(state == statesOfGrowth.Length)
                    {
                        //Si on arrive � la derni�re �tape on dit que la culture est r�coltable
                        isHarvestable = true;
                    }
                }
            }
        }
    }

    //Fonction pour la plantation de graines -> On renseigne tout les champ n�cessaire
    public void isSeedeed(SeedData seedData)
    {
        statesOfGrowth = seedData.statesOfGroth;
        type = seedData.typeOfSeed;
        dayBeforeGrowth = seedData.dayBeforeGrowth;
        isPlanted = true;
        plantType = seedData.plantType;
    }

    //Fonction pour le ramassage de la culture
    public void isPickedUp()
    {
        isHarvestable = false;
        if (plantType == PlantType.Plant)
        {
            Debug.Log("C'est un plant: "+state);
            state--;
            Destroy(actualPrefab);
            actualPrefab = Instantiate(statesOfGrowth[state-1], gameObject.transform);
        }
        else
        {
            Debug.Log("C'est autre chose");
            Destroy(actualPrefab);
            Reinitialised();
        }
    }

    //Fonction pour enlever la culture et r�initialiser la terre
    public void Reinitialised()
    {
        statesOfGrowth = null;
        type = null;
        dayBeforeGrowth = 0;
        dayTracker = 0;
        isPlanted = false;
        dirt.Reinisialised();
    }
}
