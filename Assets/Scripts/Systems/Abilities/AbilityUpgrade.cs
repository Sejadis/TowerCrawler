using System;
using SejDev.Editor;
using UnityEngine;

namespace SejDev.Systems.Abilities
{
    public abstract class AbilityUpgrade : ScriptableObject
        // public abstract class Upgrade<T> : ScriptableObject where T: Ability
    {
        [field: SerializeField, Rename] public string Name { get; private set; }
        [field: SerializeField, Rename] public Sprite Icon { get; private set; }
        [field: SerializeField, Rename] public string Description { get; private set; }

        protected Ability ability;
        private Guid guid;
        [SerializeField] private string id;
        public string GUID => id;
        public bool IsActive { get; private set; }

        public virtual void Bind(Ability ability)
        {
            this.ability = ability;
        }

        public virtual void Activate()
        {
            IsActive = true;
        }

        public virtual void DeActivate()
        {
            IsActive = false;
        }

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