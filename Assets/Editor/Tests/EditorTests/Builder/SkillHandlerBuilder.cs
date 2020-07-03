using SejDev.Systems.Skills;

namespace Editor.Tests.EditorTests.Builder
{
    public class SkillHandlerBuilder
    {
        ISkillEffect skillEffect;
        float remainingCooldown;
        public SkillHandlerBuilder(ISkillEffect skillEffect)
        {
            this.skillEffect = skillEffect;
        }

        public SkillHandlerBuilder WithRemainingCooldown(float remainingCooldown)
        {
            this.remainingCooldown = remainingCooldown;
            return this;
        }

        public SkillHandler Build()
        {
            return new SkillHandler(skillEffect,remainingCooldown);
        }

        public static implicit operator SkillHandler(SkillHandlerBuilder skillHandlerBuilder)
        {
            return skillHandlerBuilder.Build();
        }
    }
}