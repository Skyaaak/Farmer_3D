using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Références aux boutons
    [SerializeField] 
    public GameObject btnJouer;

    [SerializeField]
    public GameObject btnCommandes;

    [SerializeField]
    public GameObject btnQuitter;

    // Référence au Canvas des commandes
    public GameObject memoCommandes;


    public void jouer() {
        // Charger la scène "SampleScene"
        SceneManager.LoadScene("SampleScene");
    }

    public void afficherMemoCommandes() {
        // Afficher le Canvas des commandes
        if (memoCommandes != null) {
            memoCommandes.SetActive(true);
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
