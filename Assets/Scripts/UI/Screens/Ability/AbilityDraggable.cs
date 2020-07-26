using System.Collections;
using System.Collections.Generic;
using SejDev.Systems.Abilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityDraggable : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Ability ability;

    public Ability Ability
    {
        get => ability;
        set
        {
            ability = value;
            UpdateIcon();
        }
    }

    [SerializeField] private Image iconImage;

    private void UpdateIcon()
    {
        iconImage.sprite = ability.Icon;
    }

    public void OnDrag(PointerEventData eventData)
    {
        eventData.pointerDrag.transform.position = Input.mousePosition;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(eventData.pointerDrag);
    }
}