using UnityEngine;

public sealed class PowerProperty : Property<float>
{
    private float target;

    private void Awake()
    {
        target = Value;
    }

#if UNITY_EDITOR
    private new void Update()
    {
        base.Update();
#else
    private void Update()
    {
#endif

        Value = Mathf.Clamp01(target);
        target = 0;
    }

    public void Apply(float value)
    {
        target += value;
    }
}
