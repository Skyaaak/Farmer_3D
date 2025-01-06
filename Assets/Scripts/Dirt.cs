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

    //Fonction pour labourr� la terre
    public void isGettingPlowed()
    {
        plowed = true;
        //On cative le prefab de la terre labourr� et on d�sactive le pr�fab de terre non labour�
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
