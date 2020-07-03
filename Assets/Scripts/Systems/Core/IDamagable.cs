using System;

namespace SejDev.Systems.Core
{
    public interface IDamagable
    {
        event EventHandler<DamageHandlerEventArgs> OnPreDamage;
        event EventHandler<DamageHandlerEventArgs> OnPostDamage;

        void TakeDamage(object source, int amount, DamageFlags options = default);
    }
}
