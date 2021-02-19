using SejDev.Systems.Abilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityScreenSlot : MonoBehaviour, IDropHandler
{
    private Ability ability;

    public Ability Ability
    {
        get { return ability; }
        set
        {
            ability = value;
            if (ability != null)
            {
                SetVisuals();
            }
        }
    }

    private void SetVisuals()
    {
        if (Ability.Icon != null)
        {
            iconImage.sprite = Ability.Icon;
        }
    }

    [SerializeField] private AbilitySlot slot;

    [SerializeField] private Image iconImage;

    public void OnDrop(PointerEventData eventData)
    {
        var draggable = eventData.pointerDrag.GetComponent<Draggable>();
        if (draggable != null)
        {
            Ability = draggable.Describable as Ability; //TODO proper type checking or separate field for payload
            AbilityManager.SetAbility(Ability, slot);
        }

        Destroy(eventData.pointerDrag);
    }
}