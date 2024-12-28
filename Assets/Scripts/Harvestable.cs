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

    public void AddDay()
    {
        if (isPlanted)
        {
            dayTracker++;
            if (dayTracker % dayBeforeGrowth == 0)
            {
                if (state == 0)
                {
                    state++;
                    actualPrefab = Instantiate(statesOfGroth[0], gameObject.transform);
                }
                else
                {
                    if (state < statesOfGroth.Length)
                    {
                        Destroy(actualPrefab);
                        actualPrefab = Instantiate(statesOfGroth[state], gameObject.transform);
                        state++;
                    }
                    if(state == statesOfGroth.Length)
                    {
                        isHarvestable = true;
                    }
                }
            }
            /*
            if (dayTracker == 1)
            {
                actualPrefab = Instantiate(statesOfGroth[0], gameObject.transform);
            }
            else
            {
                if(dayTracker < statesOfGroth.Length + 1)
                {
                    state++;
                    Destroy(actualPrefab);
                    actualPrefab = Instantiate(statesOfGroth[dayTracker -1], gameObject.transform);
                }
                
            }*/
        }
    }

    public void isSeedeed(SeedData seedData)
    {
        statesOfGroth = seedData.statesOfGroth;
        type = seedData.typeOfSeed;
        dayBeforeGrowth = seedData.dayBeforeGrowth;
        isPlanted = true;
    }
}
