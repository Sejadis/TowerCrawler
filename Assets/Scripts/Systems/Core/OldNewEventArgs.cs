using System;

public class OldNewEventArgs<T> : EventArgs
{
    public OldNewEventArgs(T oldValue, T newValue)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }

    public T OldValue { get; set; }

    public T NewValue { get; set; }
}