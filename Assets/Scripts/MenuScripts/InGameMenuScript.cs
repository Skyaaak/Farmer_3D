using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.VisualScripting.Icons;

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
        Inventory inventaire = FindAnyObjectByType<Inventory>();
        SaveInventoryManager.SaveJsonData(new List<ISaveable> { inventaire });
        saveText.text = LanguageManager.Instance.GetTranslation("saveSuccess");

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerPrefs.SetFloat("playerX", player.transform.position.x);
        PlayerPrefs.SetFloat("playerY", player.transform.position.y);
        PlayerPrefs.SetFloat("playerZ", player.transform.position.z);
        PlayerPrefs.SetFloat("playerRotationY", player.transform.rotation.y);
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        PlayerPrefs.SetFloat("cameraX", camera.transform.position.x);
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
