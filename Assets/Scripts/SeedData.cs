using UnityEngine;

// Classe contenant les donn�es des graines
[CreateAssetMenu(fileName = "seed", menuName = "seed/New seed")]
public class SeedData : ScriptableObject
{
    [SerializeField]
    public GameObject[] statesOfGroth;
    [SerializeField]
    public string typeOfSeed;
    [SerializeField]
    public int dayBeforeGrowth;
    [SerializeField]
    public PlantType plantType;

}

public enum PlantType
{
    Plant,
    Tearable
}