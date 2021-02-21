using SejDev.Systems.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private IDescribable describable;
    public IBeginDragHandler Source { get; set; }

    public IDescribable Describable
    {
        get => describable;
        set
        {
            describable = value;
            UpdateIcon();
        }
    }

    [SerializeField] private Image iconImage;

    private void UpdateIcon()
    {
        iconImage.sprite = describable.Icon;
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