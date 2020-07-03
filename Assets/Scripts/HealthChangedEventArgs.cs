using System;

namespace SejDev
{
    public class HealthChangedEventArgs : EventArgs
    {
        public int OldHealth { get; set; }
        public int NewHealth { get; set; }

        public HealthChangedEventArgs(int oldHealth, int newHealth)
        {
            this.OldHealth = oldHealth;
            this.NewHealth = newHealth;
        }
    }
}
