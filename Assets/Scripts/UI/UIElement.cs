using System;
using SejDev.Systems.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SejDev.UI
{
    public class UIElement : MonoBehaviour, IBeginDragHandler, IDragHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Image icon;
        [SerializeField] protected GameObject dragPrefab;
        private IDescribable payload;
        private Transform dragParent;

        public event EventHandler<IDescribable> OnElementEnter;
        public event EventHandler<IDescribable> OnElementExit;
        public event EventHandler<IDescribable> OnElementClicked;

        public void Bind(IDescribable payload, Transform dragParent)
        {
            this.payload = payload;
            this.dragParent = dragParent;
            SetVisuals();
        }

        private void SetVisuals()
        {
            icon.sprite = payload?.Icon;
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (payload == null) return;
            var go = Instantiate(dragPrefab, dragParent, true);
            go.GetComponent<Draggable>().Describable = payload;
            go.transform.localScale = Vector3.one;
            go.transform.rotation = new Quaternion(0, 0, 0, 0);
            eventData.pointerDrag = go;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            OnElementEnter?.Invoke(this, payload);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            OnElementExit?.Invoke(this, payload);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            OnElementClicked?.Invoke(this, payload);
        }
    }
}