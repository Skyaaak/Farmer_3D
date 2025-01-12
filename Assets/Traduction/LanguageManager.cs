using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    private static LanguageManager manager;
    private string currentLanguage = "en";
    private Dictionary<string, Dictionary<string, string>> translations = new Dictionary<string, Dictionary<string, string>>()
    {
        { "en", new Dictionary<string, string> {
                { "welcome", "Welcome in Farmer 3D!" },
                { "play", "Play" },
                { "exit", "Exit" },
                { "backMenu", "Back to Menu" },
                { "commands", "Memo of the commands" },
                { "forward", "W to move forward" },
                { "moveBack", "S to move back" },
                { "right", "D to move right" },
                { "left", "A to move left" },
                { "showMenu", "Q to show the menu" },
                { "pickup", "E to pick up an item"}
            }
        },
        { "fr", new Dictionary<string, string> {
                { "welcome", "Bienvenue sur Farmer 3D!" },
                { "play", "Jouer" },
                { "exit", "Quitter" },
                { "backMenu", "Retour au menu" },
                { "commands", "Mémos des commande" },
                { "forward", "Z pour avancer" },
                { "moveBack", "S pour reculer" },
                { "right", "D pour aller à droite" },
                { "left", "Q pour aller à gauche" },
                { "showMenu", "A pour afficher le menu" },
                { "pickup", "E pour ramasser un objet"}
            }
        },
    };

    public static LanguageManager Instance
    {
        get
        {
            if (manager == null)
                manager = FindObjectOfType<LanguageManager>();
            return manager;
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
