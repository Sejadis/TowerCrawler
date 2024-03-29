﻿using System;

namespace SejDev.Systems.Stats
{
    [Serializable]
    public class Modifier
    {
        [NonSerialized] public object source;
        public ModifierType type;
        public float value;
        public bool ignoreRestriction;

        public Modifier(ModifierType type, float value, object source, bool ignoreRestriction = false)
        {
            this.type = type;
            this.value = value;
            this.source = source;
            this.ignoreRestriction = ignoreRestriction;
        }

        public override string ToString()
        {
            return $"+ {value}";
        }
    }
}