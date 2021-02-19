using System;
using SejDev.Systems.Gear;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SejDev.UI.Screens.Inventory
{
    public class ItemElement : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerClickHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image icon;
        [SerializeField] private GameObject tooltip;
        [SerializeField] private GameObject dragPrefab;
        [SerializeField] private Item item;
        private Transform dragParent;

        public event EventHandler<Item> OnElementClicked;
        public event EventHandler<Item> OnElementEnter;
        public event EventHandler<Item> OnElementExit;

        public void Bind(Item inventoryItem, GameObject tooltip, Transform dragParent)
        {
            item = inventoryItem;
            this.tooltip = tooltip;
            this.dragParent = dragParent;
            SetVisuals();
        }


        private void SetVisuals()
        {
            if (item != null)
            {
                icon.sprite = item.Icon;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (item == null) return;
            var go = Instantiate(dragPrefab, dragParent, true);
            go.GetComponent<Draggable>().Describable = item;
            go.transform.localScale = Vector3.one;
            go.transform.rotation = new Quaternion(0, 0, 0, 0);
            eventData.pointerDrag = go;
        }

        public void OnDrag(PointerEventData eventData)
        {
            // eventData.pointerDrag.transform.rotation = Quaternion.identity;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnElementClicked?.Invoke(this, item);
            // ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.pointerClickHandler);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnElementEnter?.Invoke(this, item);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnElementExit?.Invoke(this, item);
        }
    }
}