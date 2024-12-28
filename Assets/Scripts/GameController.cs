using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public int days = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            NewDay();
        }
    }

    public void NewDay()
    {
        days++;
        GameObject[] listeOfHarvetable = GameObject.FindGameObjectsWithTag("CapsuleDirt");
        foreach(GameObject t in listeOfHarvetable)
        {
            HarvestableInstance script = t.GetComponent<HarvestableInstance>();
            if (script != null)
            {
                script.AddDay();
            }
        }
    }
}
