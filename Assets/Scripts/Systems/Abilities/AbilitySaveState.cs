using System;

namespace SejDev.Systems.Abilities
{
    [Serializable]
    public class AbilitySaveState
    {
        public string id;
        public bool isUnlocked;


        public AbilitySaveState(string id, bool isUnlocked)
        {
            this.id = id;
            this.isUnlocked = isUnlocked;
        }
    }
}