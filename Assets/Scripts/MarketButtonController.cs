using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketButtonController : MonoBehaviour
{
    [SerializeField]
    public GameObject menuMarket;
    [SerializeField]
    private GameObject menuInventaire;
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private TextMeshProUGUI amountText;
    [SerializeField]
    private Button validButton;
    [SerializeField]
    private Button cancelButton;

    public void Start()
    {
        amountText.text = LanguageManager.Instance.GetTranslation("sellInventory") + inventory.GetSellAmount();
        var textValid = validButton.GetComponentInChildren<TextMeshProUGUI>();
        var textCancel = cancelButton.GetComponentInChildren<TextMeshProUGUI>();
        textValid.text = LanguageManager.Instance.GetTranslation("sell");
        textCancel.text = LanguageManager.Instance.GetTranslation("cancel");
    }

    //Fonction pour le click du bouton quitter
    public void ValidateButtonClick()
    {
        Debug.Log("Validate button clicked");
        //On d�sactive le menu de nuit et on r�active l'affichage de l'inventaire
        menuMarket.SetActive(false);
        menuInventaire.SetActive(true);
        //On d�sactive le curseur et on ractive le mouvement de cam�ra
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        Debug.Log("Validate button clicked");
        inventory.Sell();
    }

    public void CancelButtonClick()
    {
        menuMarket.SetActive(false);
        menuInventaire.SetActive(true);
        //On d�sactive le curseur et on ractive le mouvement de cam�ra
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}
