using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private int days = 1;
    private int moneyWin = 0;
    [SerializeField]
    private TextMeshProUGUI moneyText;
    [SerializeField]
    private TextMeshProUGUI textJourInventaire;

    public void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (PlayerPrefs.HasKey("playerX"))
        {
            var actualPlayerRotation = player.transform.rotation;
            player.transform.position = new Vector3(PlayerPrefs.GetFloat("playerX"), PlayerPrefs.GetFloat("playerY"), PlayerPrefs.GetFloat("playerZ"));
            player.transform.rotation = new Quaternion(actualPlayerRotation.x, PlayerPrefs.GetFloat("playerRotationY"), actualPlayerRotation.z, actualPlayerRotation.w);
            days = PlayerPrefs.GetInt("actualDays");
            moneyWin = PlayerPrefs.GetInt("moneyWin");
            moneyText.text = MainManager.Instance.GetMoney().ToString();
            textJourInventaire.text = days.ToString();
        }
    }

    //Fonction pour le changement de jour
    public void NewDay()
    {
        days++;
        moneyWin = 0;
        //On liste tout les terrains cultivables de la sc�ne
        GameObject[] listeOfCultivable = GameObject.FindGameObjectsWithTag("CapsuleDirt");
        foreach(GameObject cultivable in listeOfCultivable)
        {
            //On r�cup�re le script du plant
            HarvestableInstance script = cultivable.GetComponent<HarvestableInstance>();
            //Si on as quelque chose de plant�, on ajoute un jour � la culture.
            if (script != null)
            {
                script.AddDay();
            }
        }

        //On r�cup�re la liste des arbres fruitier
        GameObject[] listeOfFruitedTree = GameObject.FindGameObjectsWithTag("TreeLand");
        foreach (GameObject fruitedTree in listeOfFruitedTree)
        {
            //On r�cup�re le script du plant
            TreeLand script = fruitedTree.GetComponent<TreeLand>();
            //Si on as quelque chose de plant�, on ajoute un jour � l'arbre'.
            if (script != null)
            {
                script.AddDay();
            }
        }
    }

    public void AddMoney(int amount)
    {
        moneyWin += amount;
        MainManager.Instance.AddMoney(amount);
        moneyText.text = MainManager.Instance.GetMoney().ToString();
    }

    public void SpendMoney(int amount)
    {
        MainManager.Instance.SpendMoney(amount);
        moneyText.text = MainManager.Instance.GetMoney().ToString();
    }

    public int GetMoneyWin()
    {
        return moneyWin;
    }

    public int GetDays()
    {
        return days;
    }

}
