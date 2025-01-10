using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    void OnGUI()
    {
        const int buttonWidth = 200;
        const int buttonHeight = 50;
        
        if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2, Screen.height / 2 - buttonHeight / 2, buttonWidth, buttonHeight), "Jouer"))
        {
            // Charger la sc�ne du jeu lorsque le bouton est cliqu�.
            SceneManager.LoadScene("SampleScene");
        }

        // Cr�er 3 boutons : jouer, commandes, quitter
        // Jouer lance la sc�ne SampleScene (peut etre la renommer ?)
        // Commande lance la sc�ne CommandesSc�nes que j'impl�menterai
        // Quitter quitte le jeu
    }
}
