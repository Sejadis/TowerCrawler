using System;
using SejDev.Systems.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SejDev.UI
{
    public class UIElement : MonoBehaviour, IBeginDragHandler, IDragHandler,
        IPointerEnterHandler, IPointerExitHandler, IDropHandler, IPointerClickHandler
    {
        [SerializeField] protected Image icon;
        [SerializeField] protected GameObject dragPrefab;
        protected IDescribable payload;
        private Transform dragParent;
        public event EventHandler<(IBeginDragHandler, IDescribable)> OnElementDropped;

        public event EventHandler<IDescribable> OnElementEnter;
        public event EventHandler<IDescribable> OnElementExit;
        public event EventHandler<IDescribable> OnElementClicked;

        protected virtual void RaiseOnElementDropped(IBeginDragHandler dragHandler, IDescribable describable)
        {
            OnElementDropped?.Invoke(this, (dragHandler, describable));
        }

        protected virtual void RaiseOnElementEnter(IDescribable e)
        {
            OnElementEnter?.Invoke(this, e);
        }

        protected virtual void RaiseOnElementExit(IDescribable e)
        {
            OnElementExit?.Invoke(this, e);
        }

        protected virtual void RaiseOnElementClicked(IDescribable e)
        {
            OnElementClicked?.Invoke(this, e);
        }

        public void Bind(IDescribable payload, Transform dragParent = null)
        {
            this.payload = payload;
            Bind(dragParent);
        }

        public void Bind(Transform dragParent)
        {
            this.dragParent = dragParent;
            SetVisuals();
        }

        protected void SetVisuals()
        {
            icon.sprite = payload?.Icon;
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (payload == null) return;
            var go = Instantiate(dragPrefab, dragParent, false);
            var draggable = go.GetComponent<Draggable>();
            draggable.Describable = payload;
            draggable.Source = this;
            eventData.pointerDrag = go;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            RaiseOnElementEnter(payload);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            RaiseOnElementExit(payload);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            RaiseOnElementClicked(payload);
        }

        public virtual void OnDrop(PointerEventData eventData)
        {
            var draggable = eventData.pointerDrag.GetComponent<Draggable>();
            RaiseOnElementDropped(draggable.Source, draggable.Describable);
        }
    }
}