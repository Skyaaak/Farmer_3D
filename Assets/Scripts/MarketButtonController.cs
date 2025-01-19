using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Classe pour le contr�le du bouton de la fen�tre de vente
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

    //Fonction pour le d�marrage de la fen�tre de vente
    public void Start()
    {
        //On change les textes en fonction de la langue et on affiche le montant total de la vente
        amountText.text = LanguageManager.Instance.GetTranslation("sellInventory") + inventory.GetSellAmount();
        var textValid = validButton.GetComponentInChildren<TextMeshProUGUI>();
        var textCancel = cancelButton.GetComponentInChildren<TextMeshProUGUI>();
        textValid.text = LanguageManager.Instance.GetTranslation("sell");
        textCancel.text = LanguageManager.Instance.GetTranslation("cancel");
    }

    //Fonction pour le click du bouton quitter
    public void ValidateButtonClick()
    {
        inventory.Sell();
        //On d�sactive le menu de vente et on r�active l'affichage de l'inventaire
        menuMarket.SetActive(false);
        menuInventaire.SetActive(true);
        //On d�sactive le curseur et on ractive le mouvement de cam�ra
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    //Fonction pour le click du bouton annuler
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
