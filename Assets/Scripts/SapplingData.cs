using UnityEngine;

// Classe contenant les donn�es des jeunes pousses
[CreateAssetMenu(fileName = "sappling", menuName = "sappling/New sappling")]
public class SapplingData : ScriptableObject
{
    [SerializeField]
    public GameObject[] statesOfGrowth;
    [SerializeField]
    private string typeOfSappling;
    [SerializeField]
    private int dayBeforeGrowth;

    //Fonction permettant de r�cup�rer le nombre de jours avant la croissance
    public int getDayBeforeGrowth()
    {
        return dayBeforeGrowth;
    }

    //Fonction permettant de r�cup�rer le type de jeune pousse
    public string getTypeOfSappling() { return typeOfSappling; }
}
