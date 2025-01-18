using System;
using System.Collections.Generic;

namespace EconomicSystem.MoneySystem
{
    public interface MarketInteractor
    {
        public void Sell();
        public void GetSellAmount();
        public List<itemDetail> GetSellDetail();
    }

    public struct itemDetail
    {
        public ItemData item;
        public int amount;
    }
}