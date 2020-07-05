using System;

namespace SejDev.Systems.Stats
{
    [Serializable]
    public class Modifier
    {
        public object source;
        public ModifierType type;
        public float value;
        public bool ignoreRestriction;

        public Modifier(ModifierType type, float value, object source, bool ignoreRestriction)
        {
            this.type = type;
            this.value = value;
            this.source = source;
            this.ignoreRestriction = ignoreRestriction;
        }
    }
}