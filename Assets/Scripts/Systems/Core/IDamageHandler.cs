namespace SejDev.Systems.Core
{
    public interface IDamageHandler
    {
        //returns damage amount according to args
        int HandleDamage(DamageHandlerEventArgs args);
    }
}
