namespace SejDev.Systems.Skills
{
    public interface ISkillEffect
    {
        int Cooldown { get; set; }

        void Activate();
    }
}