using System;
using SejDev.Systems.Core;
using SejDev.Systems.Equipment;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SejDev.UI.Screens.Inventory
{
    public class CurrencyElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private CurrencyData currencyData;

        public event EventHandler<IDescribable> OnElementEnter;
        public event EventHandler<IDescribable> OnElementExit;
        private int amount;

        public int Amount
        {
            get => amount;
            set
            {
                amount = value;
                text.text = amount.ToString();
            }
        }

        public CurrencyData CurrencyData => currencyData;

        private void Awake()
        {
            icon.sprite = currencyData.Icon;
            Amount = -1;
        }

        protected virtual void RaiseOnElementEnter(IDescribable e)
        {
            OnElementEnter?.Invoke(this, e);
        }

        protected virtual void RaiseOnElementExit(IDescribable e)
        {
            OnElementExit?.Invoke(this, e);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            RaiseOnElementEnter(currencyData);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            RaiseOnElementExit(currencyData);
        }
    }
}