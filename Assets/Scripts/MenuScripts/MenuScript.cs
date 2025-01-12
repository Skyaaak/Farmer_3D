using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] 
    public Button btnJouer;

    [SerializeField]
    public GameObject btnCommandes;

    [SerializeField]
    public GameObject btnQuitter;

    [SerializeField]
    public GameObject btnRetourMenu;

    [SerializeField]  
    public GameObject memoCommandes;

    [SerializeField] 
    public GameObject menuPrincipal;


    public void Start()
    {
        var textJouer = btnJouer.GetComponentInChildren<TextMeshProUGUI>();
        if(LanguageManager.Instance != null)
        {
            if(textJouer != null)
            {
                textJouer.text = LanguageManager.Instance.GetTranslation("play");
            }
            else
            {
                print("textJoueur est null");
            }
            
        }
        else
        {
            print("Pas de LanguageManager");
        }
        
    }

    public void jouer() {
        // Charger la scène "SampleScene"
        SceneManager.LoadScene("SampleScene");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    public void afficherMemoCommandes() {
        // Afficher le Canvas des commandes
        if (memoCommandes != null) {
            memoCommandes.SetActive(true);
        }
        //Cacher le menu
        if (menuPrincipal != null) {
            menuPrincipal.SetActive(false);
        }
    }

    public void retourMenu() {
        //Cacher les commandes
        if (memoCommandes != null){
            memoCommandes.SetActive(false);
        }
        //Afficher le menu
        if (menuPrincipal != null){
            menuPrincipal.SetActive(true);
        }
    }

    public void quitter() {
        // Quitter le jeu
        Application.Quit();

        // Note : La fonction Application.Quit() ne fonctionne pas dans l'éditeur Unity.
        // Pour tester dans l'éditeur, vous pouvez utiliser cette ligne :
        Debug.Log("Quitter le jeu !");
    }
}
