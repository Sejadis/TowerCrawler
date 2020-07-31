﻿using System;
using UnityEngine;

namespace SejDev.Systems.UI
{
    [Serializable]
    public abstract class UIScreen : MonoBehaviour
    {
        public bool IsActive => gameObject.activeInHierarchy;

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}