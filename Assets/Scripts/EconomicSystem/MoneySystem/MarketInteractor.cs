using System;
using System.Collections.Generic;

namespace EconomicSystem.MoneySystem
{
    public interface MarketInteractor
    {
        public void Sell();
        public float GetSellAmount();
        public List<itemDetail> GetSellDetail();
    }

    public struct itemDetail
    {
        public ItemData item;
        public int amount;

        public itemDetail(ItemData itemData, int count)
        {
            this.item = itemData;
            this.amount = count;
        }
    }
}