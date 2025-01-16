using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject menuInGame;
    [SerializeField]
    private GameObject inventaire;
    [SerializeField]
    private GameObject menuOption;
    [SerializeField]
    private TextMeshProUGUI saveText;
    [SerializeField]
    private Button btnResume;
    [SerializeField]
    private Button btnSave;
    [SerializeField]
    private Button btnOption;
    [SerializeField]
    private Button btnQuit;

    void Start()
    {
        saveText.text = string.Empty;
        changeLanguage();
    }

    public void onClickResume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        inventaire.SetActive(true);
        menuInGame.SetActive(false);
    }

    public void onClickOption()
    {
        menuOption.SetActive(true);
        menuInGame.SetActive(false);
    }

    public void onClickSave()
    {
        saveText.text = LanguageManager.Instance.GetTranslation("saveSuccess");
    }

    public void onClickQuit()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void changeLanguage()
    {
        var textQuitter = btnQuit.GetComponentInChildren<TextMeshProUGUI>();
        var textRetourJeu = btnResume.GetComponentInChildren<TextMeshProUGUI>();
        var textOption = btnOption.GetComponentInChildren<TextMeshProUGUI>();
        var textSauvegarder = btnSave.GetComponentInChildren<TextMeshProUGUI>();

        if (LanguageManager.Instance != null)
        {
            textOption.text = LanguageManager.Instance.GetTranslation("option");
            textRetourJeu.text = LanguageManager.Instance.GetTranslation("resume");
            textQuitter.text = LanguageManager.Instance.GetTranslation("quit");
            textSauvegarder.text = LanguageManager.Instance.GetTranslation("save");
        }
    }

}
