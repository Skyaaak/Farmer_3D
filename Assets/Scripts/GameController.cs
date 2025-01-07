using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public int days = 1;
    public int moneyWin = 0;

    // Update is called once per frame
    void Update()
    {
    }

    //Fonction pour le changement de jour
    public void NewDay()
    {
        days++;
        moneyWin = 0;
        //On liste tout les terrains cultivables de la scéne
        GameObject[] listeOfCultivable = GameObject.FindGameObjectsWithTag("CapsuleDirt");
        foreach(GameObject cultivable in listeOfCultivable)
        {
            //On récupère le script du plant
            HarvestableInstance script = cultivable.GetComponent<HarvestableInstance>();
            //Si on as quelque chose de planté, on ajoute un jour à la culture.
            if (script != null)
            {
                script.AddDay();
            }
        }
    }
}
