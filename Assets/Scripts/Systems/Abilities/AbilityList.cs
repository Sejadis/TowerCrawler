using System.Collections.Generic;
using UnityEngine;

namespace SejDev.Systems.Abilities
{
    [CreateAssetMenu(fileName = "Assets/Resources/Abilities/NewAbilityList",
        menuName = "Systems/Ability/Ability List")]
    public class AbilityList : ScriptableObject
    {
        public List<Ability> abilities = new List<Ability>();
    }
}