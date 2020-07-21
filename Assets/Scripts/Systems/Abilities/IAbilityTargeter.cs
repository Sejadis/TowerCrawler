namespace SejDev.Systems.Abilities
{
    public interface IAbilityTargeter
    {
        bool IsTargeting { get; }
        bool RequiresSeparateTargeting { get; }

        void StartTargeting();
        object GetTarget();
    }
}