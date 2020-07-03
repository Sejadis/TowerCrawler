namespace SejDev.Systems.Skills
{
    public class SkillHolder
    {
        SkillHandler[] skills;

        public SkillHolder(int skillSlots)
        {
            skills = new SkillHandler[skillSlots];
        }

        public void AddSkillToSlot(ISkillEffect skillEffect, int slot)
        {
            skills[slot] = new SkillHandler(skillEffect);
        }

        public SkillHandler GetSkillInSlot(int slot)
        {
            return skills[slot];
        }
    }
}
