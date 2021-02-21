using System;
using SejDev.Editor;
using SejDev.Systems.Core;
using UnityEditor;
using UnityEngine;

namespace SejDev.Systems.Equipment
{
    public abstract class Item : ScriptableObject, IEquatable<Item>, IDescribable
    {
        public bool Equals(Item other)
        {
            return base.Equals(other) && guid.Equals(other.guid);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Item) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ guid.GetHashCode();
            }
        }

        protected Guid guid;
        [SerializeField] protected string id;
        public string GUID => id;

        [field: Rename]
        [field: SerializeField]
        public Sprite Icon { get; protected set; }

        [field: Rename]
        [field: SerializeField]
        public string Name { get; protected set; }

        [field: Rename]
        [field: SerializeField]
        public string Description { get; protected set; }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(id))
            {
                guid = Guid.NewGuid();
                id = guid.ToString();
                EditorUtility.SetDirty(this);
            }

            if (guid == Guid.Empty)
            {
                guid = new Guid(id);
            }
        }
    }
}