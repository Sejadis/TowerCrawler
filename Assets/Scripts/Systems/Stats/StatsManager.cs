using System;
using System.Collections.Generic;
using UnityEngine;

namespace SejDev.Systems.Stats
{
    public class StatsManager : MonoBehaviour, IStats
    {
        public List<Stat> stats = new List<Stat>();
        public event EventHandler OnAnyStatChanged;

        public Stat GetStatByType(StatType type)
        {
            return stats.Find(stat => stat.Type.Equals(type));
        }

        public void RemoveModifier(StatType type, Modifier modifier)
        {
            GetStatByType(type)?.RemoveModifier(modifier);
        }

        public void AddModifier(StatType type, Modifier modifier)
        {
            GetStatByType(type)?.AddModifier(modifier);
        }

        private void OnEnable()
        {
            foreach (var stat in stats) stat.OnStatChanged += OnStatChanged;
        }

        private void OnDisable()
        {
            foreach (var stat in stats) stat.OnStatChanged -= OnStatChanged;
        }

        private void OnStatChanged(object sender, EventArgs args)
        {
            OnAnyStatChanged?.Invoke(this, null);
        }
    }
}