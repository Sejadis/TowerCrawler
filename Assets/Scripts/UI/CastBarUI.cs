using System;
using System.Collections;
using System.Collections.Generic;
using SejDev.Systems.Abilities;
using UnityEngine;
using UnityEngine.UI;

public class CastBarUI : MonoBehaviour
{
    [SerializeField] private Image castBarImage;

    [SerializeField] private GameObject channelTickTemplate;
    [SerializeField] private GameObject channelTickLayoutParent;
    [SerializeField] private List<GameObject> createdTicks;
    [SerializeField] private AbilityManager abilityManager;
    private float castTime;
    private float channelTime;
    private AbilityActivationType activationType;

    void Start()
    {
        Hide();
        channelTickLayoutParent.SetActive(false);
        castBarImage.fillAmount = 0;
        if (abilityManager != null)
        {
            Bind(abilityManager);
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }


    public void Bind(IAbility abilityHandler)
    {
        abilityHandler.OnPreAbilityActivation += OnPreAbilityActivation;
        abilityHandler.OnPostAbilityActivation += OnPostAbilityActivation;
        abilityHandler.OnAbilityInterrupted += OnAbilityInterrupted;
    }

    private void OnAbilityInterrupted(object sender, AbilityStatusEventArgs e)
    {
        e.ability.AbilityActivator.OnStatusChanged -= OnStatusChanged;
        Hide();
    }

    private void OnPostAbilityActivation(object sender, AbilityStatusEventArgs e)
    {
        e.ability.AbilityActivator.OnStatusChanged -= OnStatusChanged;
        Hide();
    }

    private void OnPreAbilityActivation(object sender, AbilityStatusEventArgs e)
    {
        IAbilityActivator abilityActivator = e.ability.AbilityActivator;
        abilityActivator.OnStatusChanged += OnStatusChanged;
        activationType = e.ability.AbilityActivationType;
        switch (e.ability.AbilityActivationType)
        {
            case AbilityActivationType.Instant:
                break;
            case AbilityActivationType.Cast:
                castTime = e.ability.CastTime;
                channelTickLayoutParent.SetActive(false);
                Show();
                break;
            case AbilityActivationType.Channel:
                channelTime = e.ability.ChannelTime;
                Show();
                break;
            case AbilityActivationType.CastChannel:
                castTime = e.ability.CastTime;
                channelTime = e.ability.ChannelTime;
                Show();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnStatusChanged(object sender, AbilityActivatorStatusChangedEventArgs e)
    {
        switch (activationType)
        {
            case AbilityActivationType.Instant:
                break;
            case AbilityActivationType.Cast:
                castBarImage.fillAmount = e.value / castTime;
                break;
            case AbilityActivationType.Channel:
                break;
            case AbilityActivationType.CastChannel:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}