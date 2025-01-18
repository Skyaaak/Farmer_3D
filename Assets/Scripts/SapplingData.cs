using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "sappling", menuName = "sappling/New sappling")]
public class SapplingData : ScriptableObject
{
    [SerializeField]
    public GameObject[] statesOfGrowth;
    [SerializeField]
    private string typeOfSappling;
    [SerializeField]
    private int dayBeforeGrowth;

    public int getDayBeforeGrowth()
    {
        return dayBeforeGrowth;
    }

    public string getTypeOfSappling() { return typeOfSappling; }
}
