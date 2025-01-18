using System;
using EconomicSystem.MoneySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MarketSystem
{
    public class UIMarketController: MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private TextMeshProUGUI text;

        private void Awake()
        {
        }

        private void Start()
        {
            text.text = "Vous allez vendre pour un total de " + inventory.GetSellAmount() + " pièces";
        }

        private void Update()
        {
        }
    }
}