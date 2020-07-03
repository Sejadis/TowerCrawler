using UnityEngine;

namespace SejDev.Systems.Skills
{
    public abstract class SkillEffect : ScriptableObject, ISkillEffect
    {
        public int Cooldown { get; set; }

        public void Activate()
        {

        }
    }
}
