using System;

namespace SejDev.Systems.Core
{
    public interface IHealable
    {
        void Heal(object source, int amount);
        event EventHandler<HealHandlerEventArgs> OnPreHeal;
        event EventHandler<HealHandlerEventArgs> OnPostHeal;
    }
}