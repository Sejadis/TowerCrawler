using System;

namespace SejDev
{
    public class HealHandlerEventArgs : EventArgs
    {
        public readonly object healSource;
        public int finalHeal;
        public int overheal;
        public readonly int healBaseValue;

        public HealHandlerEventArgs(object healSource, int healBaseValue)
        {
            this.healSource = healSource;
            this.healBaseValue = healBaseValue;
        }
    }
}