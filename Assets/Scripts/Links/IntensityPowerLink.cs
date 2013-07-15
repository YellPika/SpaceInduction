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

    private void LateUpdate()
    {
        if (intensity.Value < targetValue)
            intensity.Value = Math.Min(intensity.Value + adaptability * Time.deltaTime, targetValue);
        if (intensity.Value > targetValue)
            intensity.Value = Math.Max(intensity.Value - adaptability * Time.deltaTime, targetValue);
    }

    protected override void SetValue(float value)
    {
        targetValue = value;
    }
}
