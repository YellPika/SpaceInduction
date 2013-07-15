using UnityEngine;

[RequireComponent(typeof(Light))]
public sealed class LightColorLink : Link<ColorProperty, Color>
{
    protected override void SetValue(Color value)
    {
        light.color = value;
    }
}
