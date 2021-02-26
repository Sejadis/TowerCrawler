using System;
using SejDev.Systems.Abilities;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace SejDev.UI
{
    public class AbilitySlot : MonoBehaviour
    {
        [SerializeField] private Ability ability;

        [SerializeField] private Image icon;
        [SerializeField] private GameObject cooldownObject;

        [SerializeField] private TextMeshProUGUI cooldownText;
        [SerializeField] private Image cooldownImage;

        [SerializeField] private TextMeshProUGUI inputText;
        [SerializeField] private GameObject chargesObject;

        [SerializeField] private TextMeshProUGUI chargesText;
        [SerializeField] private Image chargesImage;
        private float cooldown;
        private int? charges;

        // Start is called before the first frame update
        void Awake()
        {
            icon.gameObject.SetActive(false);
            cooldownObject.SetActive(false);
            inputText.gameObject.SetActive(false);
            chargesObject.SetActive(false);
        }

        public void Bind(Ability ability)
        {
            if (this.ability != null)
            {
                if (this.ability.Type.Equals(AbilityType.Charge))
                {
                    this.ability.OnChargesChanged -= OnChargesChanged;
                    chargesObject.SetActive(false);
                }

                this.ability.OnCooldownChanged -= OnCooldownChanged;
            }

            if (ability == null)
            {
                this.ability = null;
                icon.sprite = null;
                icon.gameObject.SetActive(false);
            }
            else
            {
                this.ability = ability;
                icon.sprite = ability.Icon;
                icon.gameObject.SetActive(true);
                switch (ability.Type)
                {
                    case AbilityType.Cooldown:
                        cooldown = this.ability.Cooldown;
                        break;
                    case AbilityType.Energy:
                        break;
                    case AbilityType.Charge:
                        cooldown = this.ability.ChargeCooldown;
                        chargesObject.SetActive(true);
                        this.ability.OnChargesChanged += OnChargesChanged;
                        OnChargesChanged(this,
                            new OldNewEventArgs<int>(this.ability.CurrentCharges, this.ability.CurrentCharges));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                this.ability.OnCooldownChanged += OnCooldownChanged;
                OnCooldownChanged(this, new OldNewEventArgs<float>(0, this.ability.RemainingCooldown));
            }
        }

        private void OnChargesChanged(object sender, OldNewEventArgs<int> e)
        {
            charges = e.NewValue;
            chargesText.text = e.NewValue.ToString();
        }

        private void OnCooldownChanged(object sender, OldNewEventArgs<float> args)
        {
            if (args.NewValue > 0 && (charges == 0 || charges == null))
            {
                cooldownObject.SetActive((true));
                cooldownText.text = args.NewValue.ToString("F1");
                float fillPercent = args.NewValue / cooldown;
                cooldownImage.fillAmount = fillPercent;
                chargesImage.fillAmount = 0;
            }
            else
            {
                cooldownObject.SetActive(false);
                if (charges > 0)
                {
                    float fillPercent = args.NewValue / cooldown;
                    chargesImage.fillAmount = 1 - fillPercent;
                }
            }
        }

        public void Bind(InputBinding binding)
        {
            inputText.gameObject.SetActive(true);
            inputText.text = binding.ToDisplayString();
        }
    }
}