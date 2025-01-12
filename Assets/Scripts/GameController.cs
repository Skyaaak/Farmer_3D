using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public int days = 1;
    public int moneyWin = 0;

    //Fonction pour le changement de jour
    public void NewDay()
    {
        days++;
        moneyWin = 0;
        //On liste tout les terrains cultivables de la sc�ne
        GameObject[] listeOfCultivable = GameObject.FindGameObjectsWithTag("CapsuleDirt");
        foreach(GameObject cultivable in listeOfCultivable)
        {
            //On r�cup�re le script du plant
            HarvestableInstance script = cultivable.GetComponent<HarvestableInstance>();
            //Si on as quelque chose de plant�, on ajoute un jour � la culture.
            if (script != null)
            {
                script.AddDay();
            }
        }

        //On r�cup�re la liste des arbres fruitier
        GameObject[] listeOfFruitedTree = GameObject.FindGameObjectsWithTag("TreeLand");
        foreach (GameObject fruitedTree in listeOfFruitedTree)
        {
            //On r�cup�re le script du plant
            TreeLand script = fruitedTree.GetComponent<TreeLand>();
            //Si on as quelque chose de plant�, on ajoute un jour � l'arbre'.
            if (script != null)
            {
                script.AddDay();
            }
        }
    }
}
