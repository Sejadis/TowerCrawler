using System;
using SejDev.Systems.Abilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SejDev.UI
{
    public class AbilityElement : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerClickHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Ability ability;
        [SerializeField] private Image borderImage;
        [SerializeField] private Image iconImage;
        [SerializeField] private GameObject dragPrefab;
        public event EventHandler<Ability> OnElementClicked;
        public event EventHandler<Ability> OnElementEnter;
        public event EventHandler<Ability> OnElementExit;

        private void Start()
        {
            SetVisuals();
        }

        public void Bind(Ability ability)
        {
            this.ability = ability;
            SetVisuals();
        }

        private void SetVisuals()
        {
            if (ability != null)
            {
                iconImage.sprite = ability.Icon;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var go = Instantiate(dragPrefab, transform.parent.parent, true);
            go.GetComponent<AbilityDraggable>().Ability = ability;
            go.transform.position = Input.mousePosition;
            eventData.pointerDrag = go;
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnElementClicked?.Invoke(this, ability);
            // ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.pointerClickHandler);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnElementEnter?.Invoke(this, ability);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnElementExit?.Invoke(this, ability);
        }
    }
}