using System;

namespace SejDev.Systems.Core
{
    public interface IHealable
    {
        void Heal(int amount);
        event EventHandler OnPreHeal;
        event EventHandler OnPostHeal;
    }
}