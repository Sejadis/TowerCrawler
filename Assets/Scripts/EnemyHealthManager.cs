using System;
using System.Collections;
using SejDev.Systems.Core;
using SejDev.UI;
using UnityEngine;

namespace SejDev
{
    public class EnemyHealthManager : MonoBehaviour, IDamagable, IHealth
    {
        [SerializeField]
        private HealthUI healthUI;
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }

        public event EventHandler<HealthChangedEventArgs> OnCurrentHealthChanged;
        public event EventHandler<DamageHandlerEventArgs> OnPreDamage;
        public event EventHandler<DamageHandlerEventArgs> OnPostDamage;
        public event EventHandler<HealthChangedEventArgs> OnMaxHealthChanged;

        public void TakeDamage(object source, int amount, DamageFlags options = DamageFlags.None)
        {
            int newHealth = CurrentHealth - amount;
            newHealth = Mathf.Max(newHealth, 0);
            HealthChangedEventArgs args = new HealthChangedEventArgs(CurrentHealth, newHealth);
            CurrentHealth = newHealth;
            OnCurrentHealthChanged?.Invoke(this, args);
        }

        void Awake()
        {
            MaxHealth = 50;
            CurrentHealth = MaxHealth;
            healthUI.Bind(this);
            StartCoroutine(WaitForFixedUpdate(() => DoSomething()));
        }
        public void DoSomething() { }
        public IEnumerator WaitForFixedUpdate(Action callback)
        {
            yield return new WaitForFixedUpdate();
            callback();
        }
    }
}
