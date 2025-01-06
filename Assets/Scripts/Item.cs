using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.EconomicSystem.MarketSystem;
using Assets.Scripts.seedInstances;
using UnityEngine;

public class Item : MonoBehaviour, MarketableItem
{
    public ItemData item;
    [SerializeField] private bool marketable = false;
    [SerializeField] private MarketSystem market;


    private void Start()
    {
        if (marketable)
        {
            if (market == null)
            {
                throw new Exception("marketable market is null");
            }
            
            market.SubscribeItem(this);
        } 
    }
    
    public float GetPrice()
    {
        return this.market.AskPrice(this);
    }

    public void SetPrice(float price)
    {
        throw new Exception("Price not updatable");
    }
}
