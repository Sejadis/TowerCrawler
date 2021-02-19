using System;
using SejDev.Editor;
using SejDev.Systems.Gear;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SejDev.UI.Screens.Inventory
{
    public class GearElement : UIElement, IDropHandler
    {
        [field: Rename]
        [field: SerializeField]
        public EquipSlotType SlotType { get; private set; }

        public EventHandler<Equipment> OnEquipmentDropped;
        private Equipment equipment;

        public Equipment Equipment
        {
            get { return equipment; }
            set
            {
                equipment = value;

                SetVisuals();
            }
        }

        private void SetVisuals()
        {
            icon.sprite = Equipment?.Icon;
        }


        public void OnDrop(PointerEventData eventData)
        {
            var draggable = eventData.pointerDrag.GetComponent<Draggable>();
            if (draggable != null &&
                draggable.Describable is Equipment eq &&
                eq.EquipSlot == SlotType)
            {
                OnEquipmentDropped.Invoke(this, draggable.Describable as Equipment);
            }
        }
    }
}