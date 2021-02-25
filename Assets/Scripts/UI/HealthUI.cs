using SejDev.Systems.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SejDev.UI
{
    public class HealthUI : MonoBehaviour
    {
        //TODO remove, only for testing
        [SerializeField] private Component healthComponent;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private Image healthFillImage;
        private int maxHealth;
        private int currentHealth;

        protected void OnValidate()
        {
            if (!(healthComponent is IHealth))
                healthComponent = null;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (healthComponent == null) return;
            Bind(healthComponent as IHealth);
        }

        public void Bind(IHealth healthComponent)
        {
            healthComponent.OnCurrentHealthChanged += OnCurrentHealthChanged;
            healthComponent.OnMaxHealthChanged += OnMaxHealthChanged;
            OnMaxHealthChanged(healthComponent, new HealthChangedEventArgs(0, healthComponent.MaxHealth));
            OnCurrentHealthChanged(healthComponent, new HealthChangedEventArgs(0, healthComponent.CurrentHealth));
        }

        private void OnMaxHealthChanged(object sender, HealthChangedEventArgs e)
        {
            maxHealth = e.NewHealth;
            UpdateFillPercent();
        }

        private void OnCurrentHealthChanged(object sender, HealthChangedEventArgs e)
        {
            currentHealth = e.NewHealth;
            healthText.text = e.NewHealth.ToString();
            UpdateFillPercent();
        }

        private void UpdateFillPercent()
        {
            float fillPercent = (float) currentHealth / maxHealth;
            healthFillImage.fillAmount = fillPercent;
        }
    }
}