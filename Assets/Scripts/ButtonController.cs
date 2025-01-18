using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuDeNuit;
    [SerializeField]
    private GameObject inventaire;
    [SerializeField]
    private Transform playerBody;

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

        playerBody.rotation = Quaternion.Euler(new Vector3(0f, -120f, 0f)); ;
    }
}
