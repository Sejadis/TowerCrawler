namespace SejDev.Systems.Skills
{
    public class SkillHandler
    {
        public ISkillEffect SkillEffect {get; private set; }
        public bool IsOnCooldown => RemainingCooldown > 0;
        public float RemainingCooldown {get; private set; }
        public SkillHandler(ISkillEffect skillEffect, float remainingCooldown = 0)
        {
            this.SkillEffect = skillEffect;
            RemainingCooldown = remainingCooldown;
        }

        public void Activate()
        {
            if(RemainingCooldown > 0){
                return;
            }

            RemainingCooldown = SkillEffect.Cooldown;

            SkillEffect.Activate();
        }
    }
}
