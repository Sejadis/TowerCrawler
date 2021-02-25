using System;
using SejDev.Player;
using SejDev.Systems.Core;
using SejDev.Systems.Stats;
using UnityEngine;

namespace SejDev
{
    public class HealthManager : MonoBehaviour, IDamagable, IHealable, IHealth
    {
        private int maxHealth;
        private int currentHealth;

        public int MaxHealth
        {
            get => maxHealth;
            private set
            {
                var args = new HealthChangedEventArgs(maxHealth, value);
                maxHealth = value;
                OnMaxHealthChanged?.Invoke(this, args);
            }
        }

        public int CurrentHealth
        {
            get => currentHealth;
            private set
            {
                var args = new HealthChangedEventArgs(currentHealth, value);
                currentHealth = value;
                OnCurrentHealthChanged?.Invoke(this, args);
            }
        }

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
            StatsManager statsManager = GetComponent<StatsManager>();
            if (statsManager != null)
            {
                Stat healthStat = statsManager.GetStatByType(StatType.Health);
                if (healthStat != null)
                {
                    healthStat.OnStatChanged += (_, args) =>
                    {
                        var percent = CurrentHealth / (float) MaxHealth;
                        MaxHealth = (int) args.NewValue;
                        CurrentHealth = Mathf.CeilToInt(MaxHealth * percent);
                    };
                    MaxHealth = (int) healthStat.Value;
                }
            }

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