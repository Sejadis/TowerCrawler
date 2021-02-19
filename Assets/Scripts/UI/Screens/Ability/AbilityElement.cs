﻿using System;
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
        private Transform dragParent;
        private bool isDragging;
        private GameObject dragObj;
        public event EventHandler<Ability> OnElementClicked;
        public event EventHandler<Ability> OnElementEnter;
        public event EventHandler<Ability> OnElementExit;

        private void Start()
        {
            SetVisuals();
        }

        public void Bind(Ability ability, Transform dragParent)
        {
            this.dragParent = dragParent;
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
            var go = Instantiate(dragPrefab, dragParent, true);
            go.GetComponent<Draggable>().Describable = ability;
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