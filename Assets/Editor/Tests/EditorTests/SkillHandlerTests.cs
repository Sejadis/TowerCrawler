using Editor.Tests.EditorTests.Builder;
using NSubstitute;
using NUnit.Framework;
using SejDev.Systems.Skills;

namespace Editor.Tests.EditorTests
{
    public class SkillHandlerTests
    {

        [Test]
        public void Activate_Skill_Without_Cooldown_Does_Not_Trigger_Cooldown()
        {
            ISkillEffect skillEffect = Substitute.For<ISkillEffect>();
            skillEffect.Cooldown.Returns(0);
            SkillHandler sKillHandler = A.SkillHandler(skillEffect);

            sKillHandler.Activate();

            Assert.IsFalse(sKillHandler.IsOnCooldown);
        }

        [Test]
        public void Activate_Skill_With_Cooldown_Triggers_Cooldown()
        {
            ISkillEffect skillEffect = Substitute.For<ISkillEffect>();
            skillEffect.Cooldown.Returns(1);
            SkillHandler sKillHandler = A.SkillHandler(skillEffect);

            sKillHandler.Activate();

            Assert.IsTrue(sKillHandler.IsOnCooldown);
        }

        [Test]
        public void SkillEffect_Is_Not_Activated_While_On_Cooldown()
        {
            ISkillEffect skillEffect = Substitute.For<ISkillEffect>();
            SkillHandler sKillHandler = A.SkillHandler(skillEffect).WithRemainingCooldown(1);

            sKillHandler.Activate();

            skillEffect.DidNotReceive().Activate();
        }
    }
}
