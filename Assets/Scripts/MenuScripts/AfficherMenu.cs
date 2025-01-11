using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfficherMenu : MonoBehaviour
{
    void Update()
    {
        // Vérifier si la touche "A" est pressée
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Charger la scène du menu
            SceneManager.LoadScene("MenuScene");

            //On désactive le mouvement de la caméra et on réactive la souris
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
