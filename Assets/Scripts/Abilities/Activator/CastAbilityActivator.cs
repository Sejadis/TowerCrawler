﻿using System;
using System.Collections;
using JetBrains.Annotations;
using SejDev.Systems.Abilities;
using UnityEngine;

namespace SejDev.Abilities.Activator
{
    public class CastAbilityActivator : IAbilityActivator
    {
        public bool IsActive => isCasting;
        public event EventHandler<AbilityActivatorStatusChangedEventArgs> OnStatusChanged;
        private Action callback;
        private MonoBehaviour routineBase;
        private readonly float castTime;
        private bool isCasting;
        private Coroutine coroutine;

        public CastAbilityActivator([NotNull] Action callback, float castTime, [NotNull] MonoBehaviour routineBase)
        {
            this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
            this.castTime = castTime;
            this.routineBase = routineBase ?? throw new ArgumentNullException(nameof(routineBase));
        }

        public void Activate(float activationModifier)
        {
            coroutine = routineBase.StartCoroutine(WaitForCastTime(activationModifier));
        }

        public void Interrupt()
        {
            routineBase.StopCoroutine(coroutine);
            isCasting = false;
        }

        private IEnumerator WaitForCastTime(float activationModifier)
        {
            float activeTime = 0;
            var modifiedTime = castTime * activationModifier;
            isCasting = true;
            while (activeTime < modifiedTime)
            {
                activeTime += Time.deltaTime;
                OnStatusChanged?.Invoke(this, new AbilityActivatorStatusChangedEventArgs(activeTime));
                yield return null;
            }

            callback();
            isCasting = false;
        }
    }
}