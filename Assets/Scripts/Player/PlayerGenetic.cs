using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class PlayerGenetic
    {
        [SerializeField] private float energyMaxStock;
        [SerializeField] private float energyConsumptionRate;
        [SerializeField] private float energyProductionRate;
        
        [SerializeField] private float strengh;

        [SerializeField] private float energyAffinity;
        [SerializeField] private float strenghAffinity;

        private int testIncrement = 0;

        public PlayerGenetic()
        {
            energyMaxStock = 100;
            energyConsumptionRate = 1.0f;
            energyProductionRate = 0.5f;
            
            strengh = 20;
            
            energyAffinity = 2;
            strenghAffinity = 1;
        }

        public void Update()
        {
            
        }
        
        public float EnergyMaxStock => energyMaxStock;
        public float EnergyConsumptionRate => energyConsumptionRate;
        public float EnergyProductionRate => energyProductionRate;
        public float Strengh => strengh;
        public float EnergyAffinity => energyAffinity;
        public float StrenghAffinity => strenghAffinity;

        public void SetEnergyAffinity(float value) => energyAffinity = value;
        public void SetStrenghAffinity(float value) => strenghAffinity = value;

        public void Grow()
        {
            this.energyMaxStock += energyAffinity;
            this.strengh += strenghAffinity;
        }
    }
}