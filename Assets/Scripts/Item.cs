using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData item;
    [SerializeField] private bool marketable = false;

    public bool IsMarketable()
    {
        return marketable;
    }
    
    public float GetPrice()
    {
        if (!marketable)
        {
            return 0;
        }

        return this.item.price;
    }

    public void AddPrice(float price)
    {
        if (price + item.price < 0)
        {
            throw new ArgumentException("Price cannot be negative");
        }
        
        item.price += price;
    }
}
