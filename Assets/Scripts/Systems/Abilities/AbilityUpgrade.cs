using System;
using SejDev.Editor;
using UnityEngine;

namespace SejDev.Systems.Abilities
{
    public abstract class AbilityUpgrade : ScriptableObject
    {
        [field: SerializeField, Rename] public string Name { get; private set; }
        [field: SerializeField, Rename] public Sprite Icon { get; private set; }
        [field: SerializeField, Rename] public string Description { get; private set; }

        protected Ability ability;
        private Guid guid;
        [SerializeField] private string id;

        public string GUID => id;

        public virtual void Bind(Ability ability)
        {
            this.ability = ability;
            Activate();
        }

        protected abstract void Activate();

        protected abstract void DeActivate();

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(id))
            {
                guid = Guid.NewGuid();
                id = guid.ToString();
            }

            if (guid == Guid.Empty)
            {
                guid = new Guid(id);
            }
        }
    }
}