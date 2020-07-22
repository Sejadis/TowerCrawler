using System;
using System.Collections.Generic;
using SejDev.Systems.Abilities;

namespace SejDev.Save
{
    [Serializable]
    public class AbilityStateSave : Systems.Save.Save
    {
        public readonly List<AbilitySaveState> states;

        public AbilityStateSave(List<AbilitySaveState> states)
        {
            this.states = states;
        }

        public AbilityStateSave(Dictionary<string, bool> abilityStates)
        {
            this.states = new List<AbilitySaveState>();
            foreach (var key in abilityStates.Keys)
            {
                this.states.Add(new AbilitySaveState(key, abilityStates[key]));
            }
        }
    }
}