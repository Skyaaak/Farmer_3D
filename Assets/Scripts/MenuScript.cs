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
            // Load the game scene when the button is clicked
            //Application.LoadLevel("SampleScene");
            SceneManager.LoadScene("SampleScene");
        }
    }
}
