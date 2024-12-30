using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    [SerializeField]
    public GameObject dirtObject;
    [SerializeField]
    public GameObject dirtPlowedObject;

    public bool plowed = false;

    void Update()
    {
        
    }

    //Fonction pour labourré la terre
    public void isGettingPlowed()
    {
        plowed = true;
        //On cative le prefab de la terre labourré et on désactive le préfab de terre non labouré
        dirtPlowedObject.SetActive(true);
        dirtObject.SetActive(false);
    }

    public void Reinisialized()
    {
        plowed = false;
        dirtPlowedObject.SetActive(false);
        dirtObject.SetActive(true);
    }
}
