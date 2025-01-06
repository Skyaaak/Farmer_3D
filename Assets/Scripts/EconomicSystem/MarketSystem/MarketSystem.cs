using Assets.Scripts.seedInstances;

namespace Assets.Scripts.EconomicSystem.MarketSystem
{
    public interface MarketSystem
    {
        public void SubscribeItem(MarketableItem item);
        public float AskPrice(MarketableItem item);
    }
}