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
        //On regarde si quelque chose est plant�
        if (Planted)
        {
            dayTracker++;
            //On regarrde si le nombre de jour modulo le temps entre deux �tape de pousse est �gale � 0 pour chager le mod�le
            if (dayTracker % sappling.getDayBeforeGrowth() == 0)
            {

                if (state == 0)
                {
                    //On initialise le mod�le correspondant � l'�tape actuelle
                    actualPrefab = Instantiate(sappling.statesOfGrowth[state], gameObject.transform);
                    state++;
                }
                else
                {
                    //Si la plantation n'est pas arriv� � terme on la fait avanc�e
                    if (state < sappling.statesOfGrowth.Length)
                    {
                        //Si on augmente la culture on d�truit le model actuel avant de mettre le nouveau
                        Destroy(actualPrefab);
                        actualPrefab = Instantiate(sappling.statesOfGrowth[state], gameObject.transform);
                        state++;
                    }
                    if (state == sappling.statesOfGrowth.Length)
                    {
                        //Si on arrive � la derni�re �tape on dit que la culture est r�coltable
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

    //Fonction pour enlever la culture et r�initialiser la terre
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
