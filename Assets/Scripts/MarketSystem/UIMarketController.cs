using System;
using EconomicSystem.MoneySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MarketSystem
{
    public class UIMarketController: MonoBehaviour
    {
        private MarketInteractor interactor;
        [SerializeField] private 
        [SerializeField] private TextMeshProUGUI text;

        private void Awake()
        {
            interactor = GetComponent<MarketInteractor>();
        }
        private void Update()
        {
            text.text = "Vous allez vendre pour un total de " + interactor.GetSellAmount() + " pièces";
        }
    }
}