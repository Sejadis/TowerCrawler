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
    private Ability ability;

    public Ability Ability
    {
        get { return ability; }
        set
        {
            ability = value;
            SetVisuals();
        }
    }

    private void SetVisuals()
    {
        iconImage.sprite = Ability.Icon;
    }

    [SerializeField] private AbilitySlot slot;

    [SerializeField] private Image iconImage;

    public void OnDrop(PointerEventData eventData)
    {
        var draggable = eventData.pointerDrag.GetComponent<AbilityDraggable>();
        if (draggable != null)
        {
            Ability = draggable.Ability;
            AbilityManager.SetAbility(Ability, slot);
        }

        Destroy(eventData.pointerDrag);
    }
}