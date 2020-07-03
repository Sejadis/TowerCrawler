using System;
using SejDev.Player;
using SejDev.Systems.Core;
using UnityEngine;

namespace SejDev
{
    public class HealthManager : MonoBehaviour, IDamagable, IHealable, IHealth
    {

        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }

        public event EventHandler<DamageHandlerEventArgs> OnPreDamage;
        public event EventHandler<DamageHandlerEventArgs> OnPostDamage;
        public event EventHandler OnPreHeal;
        public event EventHandler OnPostHeal;
        public event EventHandler<HealthChangedEventArgs> OnCurrentHealthChanged;
        public event EventHandler<HealthChangedEventArgs> OnMaxHealthChanged;

        private IDamageHandler damageHandler = new PlayerDamageHandler();
        void Awake()
        {
            MaxHealth = 150;
            CurrentHealth = MaxHealth;
        }

        public void Init(int maxHealth, int? currentHealth = null)
        {
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth ?? MaxHealth;
        }

        public void TakeDamage(object source, int amount, DamageFlags options = default)
        {
            int finalDamage;
            int oldHealth = CurrentHealth;

            DamageHandlerEventArgs damageHandlerEventArgs = new DamageHandlerEventArgs(amount, source, options);
            OnPreDamage?.Invoke(this, damageHandlerEventArgs);

            finalDamage = damageHandler.HandleDamage(damageHandlerEventArgs);
            damageHandlerEventArgs.finalDamage = finalDamage;
            CurrentHealth -= finalDamage;
            CurrentHealth = Mathf.Max(CurrentHealth, 0);
            if (CurrentHealth == 0)
            {
                damageHandlerEventArgs.resultedInDeath = true;
                damageHandlerEventArgs.overkill = finalDamage - oldHealth;
            }

            OnCurrentHealthChanged?.Invoke(this, new HealthChangedEventArgs(oldHealth, CurrentHealth));
            OnPostDamage?.Invoke(this, damageHandlerEventArgs);
        }

        public void Heal(int amount)
        {
            int oldHealth = CurrentHealth;
            OnPreHeal?.Invoke(this, null);

            CurrentHealth += amount;
            CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);

            OnCurrentHealthChanged?.Invoke(this, new HealthChangedEventArgs(oldHealth,CurrentHealth));
            OnPostHeal?.Invoke(this, null);
        }
    }
}
