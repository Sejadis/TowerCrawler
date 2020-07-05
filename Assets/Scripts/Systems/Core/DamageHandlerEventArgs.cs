using System;

namespace SejDev.Systems.Core
{
    public class DamageHandlerEventArgs : EventArgs
    {
        public object damageSource;
        public int finalDamage;
        public int overkill;
        public bool resultedInDeath;

        public DamageHandlerEventArgs(int damageBaseValue, object damageSource, DamageFlags options = default)
        {
            DamageBaseValue = damageBaseValue;
            this.damageSource = damageSource;
            Options = options;
        }

        public int DamageBaseValue { get; }
        public float DamageMitigationPercent { get; private set; }
        public DamageFlags Options { get; }

        public bool TryApplyMitigation(float mitigationPercent)
        {
            if (mitigationPercent > DamageMitigationPercent)
            {
                DamageMitigationPercent = mitigationPercent;
                return true;
            }

            return false;
        }
    }
}