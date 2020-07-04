using System.Collections;
using System.Collections.Generic;
using SejDev.Systems.Abilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHolderUI : MonoBehaviour
{
    //TODO remove, only for testing
    [SerializeField] private Ability ability;

    [SerializeField] private Image icon;
    [SerializeField] private GameObject cooldownObject;

    [SerializeField] private TextMeshProUGUI cooldownText;
    [SerializeField] private Image cooldownImage;

    [SerializeField] private TextMeshProUGUI inputText;
    private float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        icon.gameObject.SetActive(false);
        cooldownObject.SetActive(false);
        inputText.gameObject.SetActive(false);
    }

    public void Bind(Ability ability)
    {
        if (ability == null) return; //slot is empty
        icon.sprite = ability.Icon;
        icon.gameObject.SetActive(true);
        cooldown = ability.Cooldown;
        ability.OnCooldownChanged += OnCooldownChanged;
        OnCooldownChanged(this, new OldNewEventArgs<float>(0,ability.RemainingCooldown));
    }

    private void OnCooldownChanged(object sender, OldNewEventArgs<float> args)
    {
        if (args.NewValue > 0)
        {
            cooldownObject.SetActive((true));
            cooldownText.text = args.NewValue.ToString("F1");
            float fillPercent = args.NewValue / cooldown;
            cooldownImage.fillAmount = fillPercent;
        }
        else
        {
            cooldownObject.SetActive(false);
        }
    }
}