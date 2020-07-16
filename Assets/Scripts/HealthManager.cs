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
        public event EventHandler<HealHandlerEventArgs> OnPreHeal;
        public event EventHandler<HealHandlerEventArgs> OnPostHeal;
        public event EventHandler<HealthChangedEventArgs> OnCurrentHealthChanged;
        public event EventHandler<HealthChangedEventArgs> OnMaxHealthChanged;

        private IDamageHandler damageHandler = new PlayerDamageHandler();
        private IHealHandler healHandler = new PlayerHealHandler();

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

        public void Heal(object source, int amount)
        {
            int finalHeal;
            int oldHealth = CurrentHealth;
            HealHandlerEventArgs healHandlerEventArgs = new HealHandlerEventArgs(source, amount);
            OnPreHeal?.Invoke(this, healHandlerEventArgs);

            finalHeal = healHandler.HandleHeal(healHandlerEventArgs);
            healHandlerEventArgs.finalHeal = finalHeal;
            CurrentHealth += finalHeal;
            CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
            if (CurrentHealth == MaxHealth)
            {
                healHandlerEventArgs.overheal = Mathf.Abs(MaxHealth - oldHealth - finalHeal);
            }

            OnCurrentHealthChanged?.Invoke(this, new HealthChangedEventArgs(oldHealth, CurrentHealth));
            OnPostHeal?.Invoke(this, healHandlerEventArgs);
        }
    }
}