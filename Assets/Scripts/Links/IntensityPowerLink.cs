using System;
using UnityEngine;

[RequireComponent(typeof(IntensityProperty))]
public sealed class IntensityPowerLink : Link<PowerProperty, float>
{
    [SerializeField]
    private float adaptability = 4;

    private float targetValue;
    private IntensityProperty intensity;

    private void Awake()
    {
        intensity = GetComponent<IntensityProperty>();
        targetValue = intensity.Value;
    }

    private void Update()
    {
        var value = intensity.Value;
        if (value < targetValue)
            value = Math.Min(value + adaptability * Time.deltaTime, targetValue);
        if (value > targetValue)
            value = Math.Max(value - adaptability * Time.deltaTime, targetValue);
        intensity.Value = value;
    }

    protected override void SetValue(float value)
    {
        targetValue = value;
    }
}
