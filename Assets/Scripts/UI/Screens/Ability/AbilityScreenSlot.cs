using System.Collections;
using System.Collections.Generic;
using SejDev.Systems.Abilities;
using SejDev.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using AbilitySlot = SejDev.Systems.Abilities.AbilitySlot;

public class AbilityScreenSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Ability ability;

    [SerializeField] private AbilitySlot slot;

    [SerializeField] private Image iconImage;

    public void OnDrop(PointerEventData eventData)
    {
        ability = eventData.pointerDrag.GetComponent<AbilityDraggable>()?.Ability;
        if (ability != null)
        {
            iconImage.sprite = ability.Icon;
        }

        Destroy(eventData.pointerDrag);
    }
}