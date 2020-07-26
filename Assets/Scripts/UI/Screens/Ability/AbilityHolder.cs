using System;
using SejDev.Systems.Abilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SejDev.UI
{
    public class AbilityHolder : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        [SerializeField] private Ability ability;
        public Ability Ability => ability;
        [SerializeField] private Image borderImage;
        [SerializeField] private Image iconImage;
        [SerializeField] private GameObject dragPrefab;

        private void Start()
        {
            if (ability != null)
            {
                iconImage.sprite = ability.Icon;
            }
        }

        public void Bind(Ability ability)
        {
            this.ability = ability;
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
    }
}