using System;
using UnityEngine;

namespace EconomicSystem.MoneySystem
{
    [Serializable]
    public class PlayerPocketImpl : MonoBehaviour, MoneyInPocket
    {
        [SerializeField] private float actualMoney;
        [SerializeField] private float maxMoney;

        public PlayerPocketImpl()
        {
            actualMoney = 0;
            maxMoney = -1;
        }

        public PlayerPocketImpl(float maxMoney)
        {
            this.maxMoney = maxMoney;
            actualMoney = 0;
        }

        private void Update()
        {
            if (actualMoney > maxMoney || actualMoney < 0)
            {
                throw new Exception("Money out of range");
            }
        } 

        public void AddMoney(float amount)
        {
            if (amount + actualMoney > maxMoney)
            {
                throw new Exception("Cannot add money to pocket.");
            }
            
            actualMoney += amount;
        }

        public void RemoveMoney(float amount)
        {
            if (amount - actualMoney < 0)
            {
                throw new Exception("Cannot remove money from pocket.");
            }
            
            actualMoney -= amount;
        }

        public float GetMoney()
        {
            return actualMoney;
        }
    }
}