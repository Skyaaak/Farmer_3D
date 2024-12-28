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
                    state++;
                    //On initialise le mod�le correspondant � l'�tape actuelle
                    actualPrefab = Instantiate(statesOfGroth[0], gameObject.transform);
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
    }
}
