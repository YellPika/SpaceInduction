using UnityEngine;

public sealed class PowerProperty : Property<float>
{
    private float target;

    private void Awake()
    {
        target = Value;
    }

    private void Update()
    {
        Value = Mathf.Clamp01(target);
        target = 0;
    }

    public void Apply(float value)
    {
        target += value;
    }
}
