using SejDev.Systems.Core;

namespace SejDev.Player
{
    internal class PlayerDamageHandler : IDamageHandler
    {
        public int HandleDamage(DamageHandlerEventArgs damageHandlerEventArgs)
        {
            //remaining damage after mitigation
            float result = damageHandlerEventArgs.DamageBaseValue;
            if (!damageHandlerEventArgs.Options.HasFlag(DamageFlags.IgnoresMitigation))
            {
                result *= 1 - damageHandlerEventArgs.DamageMitigationPercent;
            }

            return (int)result;
        }
    }
}