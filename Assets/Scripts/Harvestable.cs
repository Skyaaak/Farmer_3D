using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    public GameObject[] statesOfGroth;
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
            //On regarde si le nombre de jour modulo le temps entre deux �tape de pousse est �gale � 0 pour chager le mod�le
            if (dayTracker % dayBeforeGrowth == 0)
            {

                if (state == 0)
                {
                    //On initialise le mod�le correspondant � l'�tape actuelle
                    actualPrefab = Instantiate(statesOfGroth[state], gameObject.transform);
                    state++;
                }
                else
                {
                    //Si la plantation n'est pas arriv� � terme on la fait avanc�e
                    if (state < statesOfGroth.Length)
                    {
                        //Si on augmente la culture on d�truit le model actuel avant de mettre le nouveau
                        Destroy(actualPrefab);
                        actualPrefab = Instantiate(statesOfGroth[state], gameObject.transform);
                        state++;
                    }
                    if(state == statesOfGroth.Length)
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
        statesOfGroth = seedData.statesOfGroth;
        type = seedData.typeOfSeed;
        dayBeforeGrowth = seedData.dayBeforeGrowth;
        isPlanted = true;
        plantType = seedData.plantType;
    }

    public void isPickedUp()
    {
        isHarvestable = false;
        if (plantType == PlantType.Plant)
        {
            Debug.Log("C'est un plant");
            state--;
            Destroy(actualPrefab);
            actualPrefab = Instantiate(statesOfGroth[state-1], gameObject.transform);
        }
        else
        {
            Debug.Log("C'est autre chose");
            Destroy(actualPrefab);
            Reinitialised();
        }
    }

    public void Reinitialised()
    {
        statesOfGroth = null;
        type = null;
        dayBeforeGrowth = 0;
        isPlanted = false;
        dirt.Reinisialized();
    }
}
