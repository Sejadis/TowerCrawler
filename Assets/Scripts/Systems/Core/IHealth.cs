using System;

namespace SejDev.Systems.Core
{
    public interface IHealth
    {
        int MaxHealth { get; }
        int CurrentHealth { get; }
        event EventHandler<HealthChangedEventArgs> OnCurrentHealthChanged;
        event EventHandler<HealthChangedEventArgs> OnMaxHealthChanged;
        //TODO refactor to OldNewEventArgs<int>
    }
    
}
