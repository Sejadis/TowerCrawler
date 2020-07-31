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
        var cam = GameManager.Instance.UICamera;
        var mousePos = Input.mousePosition;
        mousePos.z = 100f;
        var pos = cam.ScreenToWorldPoint(mousePos);
        eventData.pointerDrag.transform.position = pos;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(eventData.pointerDrag);
    }
}