using System.Collections.Generic;

namespace SejDev.Systems.Stats
{
    public class ModifierContainer
    {
        public List<Modifier> Modifiers { get; } = new List<Modifier>();

        //public List<Modifier> TryGetModifiersByType(ModifierType type)
        //{
        //    return 
        //}

        public float GetFinalModifierByType(ModifierType type)
        {
            float final = 0;
            foreach (var modifier in Modifiers)
                if (modifier.type.Equals(type) && !modifier.ignoreRestriction)
                    final += modifier.value;
            return final;
        }
        public float GetFinalOverridingModifierByType(ModifierType type)
        {
            float final = 0;
            foreach (var modifier in Modifiers)
                if (modifier.type.Equals(type) && modifier.ignoreRestriction)
                    final += modifier.value;
            return final;
        }
    }
}