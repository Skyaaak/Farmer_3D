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

    // Update is called once per frame
    void Update()
    {
        
    }

    //Fonction pour ajouter un jour à la plantation
    public void AddDay()
    {
        //On regarde si quelque chose est planté
        if (isPlanted)
        {
            dayTracker++;
            //On regarrde si le nombre de jour modulo le temps entre deux étape de pousse est égale à 0 pour chager le modèle
            if (dayTracker % dayBeforeGrowth == 0)
            {

                if (state == 0)
                {
                    state++;
                    //On initialise le modèle correspondant à l'étape actuelle
                    actualPrefab = Instantiate(statesOfGroth[0], gameObject.transform);
                }
                else
                {
                    //Si la plantation n'est pas arrivé à terme on la fait avancée
                    if (state < statesOfGroth.Length)
                    {
                        //Si on augmente la culture on détruit le model actuel avant de mettre le nouveau
                        Destroy(actualPrefab);
                        actualPrefab = Instantiate(statesOfGroth[state], gameObject.transform);
                        state++;
                    }
                    if(state == statesOfGroth.Length)
                    {
                        //Si on arrive à la dernière étape on dit que la culture est récoltable
                        isHarvestable = true;
                    }
                }
            }
        }
    }

    //Fonction pour la plantation de graines -> On renseigne tout les champ nécessaire
    public void isSeedeed(SeedData seedData)
    {
        statesOfGroth = seedData.statesOfGroth;
        type = seedData.typeOfSeed;
        dayBeforeGrowth = seedData.dayBeforeGrowth;
        isPlanted = true;
    }
}
