using UnityEngine;

[RequireComponent(typeof(Light))]
public sealed class LightIntensityLink : Link<IntensityProperty, float>
{
    private float baseIntensity;

    private void Awake()
    {
        baseIntensity = light.intensity;
    }

    protected override void SetValue(float value)
    {
        light.intensity = baseIntensity * value;
        light.enabled = value > 0;
    }
}
