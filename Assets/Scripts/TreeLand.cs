using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLand : MonoBehaviour
{
    private int state = 0;
    private int dayTracker = 0;
    private GameObject actualPrefab;
    private bool Planted = false;
    private bool Pickable = false;
    private SapplingData sappling;

    public void AddDay()
    {
        //On regarde si quelque chose est planté
        if (Planted)
        {
            dayTracker++;
            //On regarrde si le nombre de jour modulo le temps entre deux étape de pousse est égale à 0 pour chager le modèle
            if (dayTracker % sappling.getDayBeforeGrowth() == 0)
            {

                if (state == 0)
                {
                    //On initialise le modèle correspondant à l'étape actuelle
                    actualPrefab = Instantiate(sappling.statesOfGrowth[state], gameObject.transform);
                    state++;
                }
                else
                {
                    //Si la plantation n'est pas arrivé à terme on la fait avancée
                    if (state < sappling.statesOfGrowth.Length)
                    {
                        //Si on augmente la culture on détruit le model actuel avant de mettre le nouveau
                        Destroy(actualPrefab);
                        actualPrefab = Instantiate(sappling.statesOfGrowth[state], gameObject.transform);
                        state++;
                    }
                    if (state == sappling.statesOfGrowth.Length)
                    {
                        //Si on arrive à la dernière étape on dit que la culture est récoltable
                        Pickable = true;
                    }
                }
            }
        }
    }

    public void Plant(SapplingData newSappling)
    {
        sappling = newSappling;
        Planted = true;
    }

    public void PickUp()
    {
        Pickable = false;
        state--;
        Destroy(actualPrefab);
        actualPrefab = Instantiate(sappling.statesOfGrowth[state - 1], gameObject.transform);
    }

    //Fonction pour enlever la culture et réinitialiser la terre
    public void Reinitialised()
    {
        dayTracker = 0;
        Planted = false;
        Pickable = false;
        Destroy(actualPrefab);
    }

    public bool isPickable() {  return Pickable; }

    public bool isPlanted() { return Planted; }

    public string getTreeName()
    {
        return sappling.getTypeOfSappling();
    }

    public int daySincePlantation() {  return dayTracker; }

}
