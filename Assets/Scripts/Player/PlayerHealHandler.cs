namespace SejDev
{
    internal class PlayerHealHandler : IHealHandler
    {
        public int HandleHeal(HealHandlerEventArgs args)
        {
            return args.healBaseValue;
        }
    }
}