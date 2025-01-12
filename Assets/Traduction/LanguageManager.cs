using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LanguageManager : MonoBehaviour
{
    private static LanguageManager manager;
    private string currentLanguage = "en";
    private Dictionary<string, Dictionary<string, string>> translations;
    public UnityEngine.TextAsset fichierTrad;

    public static LanguageManager Instance
    {
        get
        {
            if (manager == null)
                manager = FindObjectOfType<LanguageManager>();
            return manager;
        }
    }

    private void LoadTranslations()
    {
        translations = new Dictionary<string, Dictionary<string, string>>();
        if (fichierTrad != null)
        { 
            string json = fichierTrad.text;
            translations = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
        } 
        else { 
            Debug.LogError("Fichier de traduction non trouvé !"); 
        }
    }

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (manager != this)
        {
            Destroy(gameObject);
            return;
        }

        LoadTranslations();
    }

    public string GetTranslation(string key)
    {
        if (translations.ContainsKey(currentLanguage) && translations[currentLanguage].ContainsKey(key))
        {
            return translations[currentLanguage][key];
        }
        return key;
    }

    public void SetLanguage(string language)
    {
        if (translations.ContainsKey(language))
        {
            currentLanguage = language;
        }
        else
        {
            Debug.LogWarning("Langue non supportée !");
        }
    }


}

[System.Serializable]
public class TranslationData
{
    public Dictionary<string, string> en;
    public Dictionary<string, string> fr;
}