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
            // Charger la scène du jeu lorsque le bouton est cliqué.
            SceneManager.LoadScene("SampleScene");
        }

        // Créer 3 boutons : jouer, commandes, quitter
        // Jouer lance la scène SampleScene (peut etre la renommer ?)
        // Commande lance la scène CommandesScènes que j'implémenterai
        // Quitter quitte le jeu
    }
}
