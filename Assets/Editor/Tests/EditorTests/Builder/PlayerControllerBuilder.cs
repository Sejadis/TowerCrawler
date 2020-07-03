using SejDev;
using UnityEngine;

namespace Editor.Tests.EditorTests.Builder
{
    public class HealthManagerBuilder
    {
        int maxHealth;
        int currentHealth;
        public HealthManagerBuilder(int maxHealth,
            int currentHealth)
        {
            this.maxHealth = maxHealth;
            this.currentHealth = currentHealth;
        }

        public HealthManagerBuilder() : this(100, 100)
        {
        }

        public HealthManagerBuilder WithMaxHealth(int maxHealth)
        {
            this.maxHealth = maxHealth;
            return this;
        }

        public HealthManagerBuilder WithCurrentHealth(int currentHealth)
        {
            this.currentHealth = currentHealth;
            return this;
        }

        public HealthManager Build()
        {
            HealthManager healthManager = new GameObject().AddComponent<HealthManager>();
            healthManager.Init(maxHealth, currentHealth);
            return healthManager;
        }

        public static implicit operator HealthManager(HealthManagerBuilder healthManagerBuilder)
        {
            return healthManagerBuilder.Build();
        }
    }
}