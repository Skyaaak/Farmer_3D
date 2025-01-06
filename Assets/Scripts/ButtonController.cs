using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    public GameObject menuDeNuit;
    [SerializeField]
    public GameObject inventaire;

    //Fonction pour le click du bouton quitter
    public void CloseButtonClick()
    {
        //On d�sactive le menu de nuit et on r�active l'affichage de l'inventaire
        menuDeNuit.SetActive(false);
        inventaire.SetActive(true);
        //On d�sactive le curseur et on ractive le mouvement de cam�ra
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}
