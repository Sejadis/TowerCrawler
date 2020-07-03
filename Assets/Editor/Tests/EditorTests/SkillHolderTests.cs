using NSubstitute;
using NUnit.Framework;
using SejDev.Systems.Skills;

namespace Editor.Tests.EditorTests
{
    public class SkillHolderTests
    {
        public class AddSkillToSlot
        {
            [Test]
            public void Skill_Is_Added_To_Slot()
            {
                SkillHolder skillHolder = new SkillHolder(1);
                ISkillEffect skillEffect = Substitute.For<ISkillEffect>();
                skillHolder.AddSkillToSlot(skillEffect, 0);

                Assert.AreEqual(skillEffect, skillHolder.GetSkillInSlot(0).SkillEffect);
            }

            [Test]
            public void Skill_Is_Not_Added_If_Skill_In_Slot_Is_On_Cooldown(){
                SkillHolder skillHolder = new SkillHolder(1);
                ISkillEffect skillEffect = Substitute.For<ISkillEffect>();
                skillHolder.AddSkillToSlot(skillEffect, 0);

                Assert.AreEqual(skillEffect, skillHolder.GetSkillInSlot(0).SkillEffect);
            }

        }
    }
}
