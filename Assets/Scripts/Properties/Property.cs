using UnityEngine;

public abstract class Property<T> : MonoBehaviour
{
    [SerializeField]
    private T value;

    public T Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
