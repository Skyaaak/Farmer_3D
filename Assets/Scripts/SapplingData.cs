using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "sappling", menuName = "sappling/New sappling")]
public class SapplingData : ScriptableObject
{
    [SerializeField]
    public GameObject[] statesOfGroth;
    [SerializeField]
    public string typeOfSappling;
    [SerializeField]
    private int dayBeforeGrowth;
}
