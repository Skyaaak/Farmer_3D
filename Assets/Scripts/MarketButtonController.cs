using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketButtonController : MonoBehaviour
{
    [SerializeField]
    public GameObject menuMarket;
    [SerializeField]
    private GameObject menuInventaire;
    [SerializeField]
    private Inventory inventory;

    //Fonction pour le click du bouton quitter
    public void ValidateButtonClick()
    {
        //On d�sactive le menu de nuit et on r�active l'affichage de l'inventaire
        menuMarket.SetActive(false);
        menuInventaire.SetActive(true);
        //On d�sactive le curseur et on ractive le mouvement de cam�ra
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}
