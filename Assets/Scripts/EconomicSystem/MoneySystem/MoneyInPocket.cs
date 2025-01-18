namespace EconomicSystem.MoneySystem
{
    public interface MoneyInPocket
    {
        public void AddMoney(float amount);
        public void RemoveMoney(float amount);
        public float GetMoney();
    }
}