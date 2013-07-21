using System;
using UnityEngine;

public abstract class Property<T> : MonoBehaviour
{
    [SerializeField]
    private T value;

#if UNITY_EDITOR
    private T previousValue;
#endif

    public T Value
    {
        get { return value; }
        set
        {
            if (this.value.Equals(value))
                return;
            
            this.value = value;

#if UNITY_EDITOR
            previousValue = value;
#endif

            if (Changed != null)
                Changed(this, new PropertyChangedEventArgs<T>(value));
        }
    }

    public event EventHandler<PropertyChangedEventArgs<T>> Changed;

#if UNITY_EDITOR
    protected void Update()
    {
        if (value.Equals(previousValue))
            return;

        previousValue = value;

        if (Changed != null)
            Changed(this, new PropertyChangedEventArgs<T>(value));
    }
#endif
}

public sealed class PropertyChangedEventArgs<T> : EventArgs
{
    public T Value { get; private set; }
    public PropertyChangedEventArgs(T value) { Value = value; }
}
