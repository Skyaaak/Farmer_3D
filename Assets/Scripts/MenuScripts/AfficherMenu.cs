using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfficherMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject inventaire;

    [SerializeField]
    private GameObject inGameMenu;

    void Update()
    {
        // Vérifier si la touche "A" est pressée
        if (Input.GetKeyDown(KeyCode.Q))
        {
            inventaire.SetActive(false);
            inGameMenu.SetActive(true);
            //On désactive le mouvement de la caméra et on réactive la souris
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
