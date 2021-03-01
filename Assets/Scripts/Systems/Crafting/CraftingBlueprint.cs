using System;
using System.Collections.Generic;
using SejDev.Editor;
using SejDev.Systems.Core;
using SejDev.Systems.Equipment;
using UnityEditor;
using UnityEngine;

namespace SejDev.Systems.Crafting
{
    [CreateAssetMenu(fileName = "Assets/Resources/Crafting/NewBlueprint",
        menuName = "Systems/Crafting/Blueprint")]
    public class CraftingBlueprint : ScriptableObject, IEquatable<CraftingBlueprint>, IDescribable
    {
        public bool Equals(CraftingBlueprint other)
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
        public Sprite Icon { get; private set; }

        [field: Rename]
        [field: SerializeField]
        public string Name { get; private set; }

        [field: Rename]
        [field: SerializeField]
        public string Description { get; private set; }

        protected virtual void OnValidate()
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

        [SerializeField] private CraftingResult craftingResult;

        //TODO prevent multiple crafting cost entries with the same currency
        [SerializeField] private List<CraftingCost> craftingCosts = new List<CraftingCost>();
        public List<CraftingCost> CraftingCosts => craftingCosts;
        public CraftingResult CraftingResult => craftingResult;
    }
}