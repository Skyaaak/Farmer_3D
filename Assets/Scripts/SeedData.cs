using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "seed", menuName = "seed/New seed")]
public class SeedData : ScriptableObject
{
    [SerializeField]
    public GameObject[] statesOfGroth;
    [SerializeField]
    public string typeOfSeed;
    [SerializeField]
    public int dayBeforeGrowth;

}
