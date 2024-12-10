using UnityEditor;
using UnityEngine;

namespace PlantSystem
{
    public class GeneticBranchImpl: MonoBehaviour
    {
        private GameObject branchObject;
        private GeneticProceduralNode childBranchNode;



        private void Grow(GeneticSystem geneticSystem, GeneticProceduralNode parentBranchNode)
        {
            Random.Range(geneticSystem.MinTrunkHeight, geneticSystem.MaxTrunkHeight);
        }
    }
}