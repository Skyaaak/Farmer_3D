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

    public void isGettingPlowed()
    {
        plowed = true;
        dirtPlowedObject.SetActive(true);
        dirtObject.SetActive(false);
    }
}
