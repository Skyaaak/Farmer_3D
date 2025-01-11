using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // R�f�rences aux boutons
    [SerializeField] 
    public GameObject btnJouer;

    [SerializeField]
    public GameObject btnCommandes;

    [SerializeField]
    public GameObject btnQuitter;

    // R�f�rence au Canvas des commandes
    public GameObject memoCommandes;


    public void jouer() {
        // Charger la sc�ne "SampleScene"
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

        // Note : La fonction Application.Quit() ne fonctionne pas dans l'�diteur Unity.
        // Pour tester dans l'�diteur, vous pouvez utiliser cette ligne :
        Debug.Log("Quitter le jeu !");
    }
}
