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
        // V�rifier si la touche "A" est press�e
        if (Input.GetKeyDown(KeyCode.Q))
        {
            inventaire.SetActive(false);
            inGameMenu.SetActive(true);
            //On d�sactive le mouvement de la cam�ra et on r�active la souris
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
