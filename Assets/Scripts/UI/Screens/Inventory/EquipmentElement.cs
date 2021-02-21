using SejDev.Editor;
using SejDev.Systems.Equipment;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SejDev.UI.Screens.Inventory
{
    public class EquipmentElement : UIElement
    {
        [field: Rename]
        [field: SerializeField]
        public EquipSlotType SlotType { get; private set; }

        public Equipment Equipment
        {
            get { return payload as Equipment; }
            set
            {
                payload = value;

                SetVisuals();
            }
        }

        public override void OnDrop(PointerEventData eventData)
        {
            var draggable = eventData.pointerDrag.GetComponent<Draggable>();
            if (draggable != null &&
                draggable.Describable is Equipment eq &&
                eq.EquipSlot == SlotType)
            {
                RaiseOnElementDropped(draggable.Source, draggable.Describable);
                Equipment = eq;
            }
        }
    }
}