using System;

namespace SejDev.Systems.Core
{
    public class DamageHandlerEventArgs : EventArgs
    {
        public int DamageBaseValue { get; }
        public float DamageMitigationPercent { get; private set; }
        public object damageSource;
        public int overkill;
        public bool resultedInDeath;
        public int finalDamage;
        public DamageFlags Options { get; }

        public DamageHandlerEventArgs(int damageBaseValue, object damageSource, DamageFlags options = default)
        {
            this.DamageBaseValue = damageBaseValue;
            this.damageSource = damageSource;
            this.Options = options;
        }

        public bool TryApplyMitigation(float mitigationPercent){
            if(mitigationPercent > DamageMitigationPercent){
                DamageMitigationPercent = mitigationPercent;
                return true;
            }
            return false;
        }
    }
}
