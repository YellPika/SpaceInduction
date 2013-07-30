using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public sealed class PowerBar : Link<PowerProperty, float>
{
    [SerializeField]
    private float adaptability = 4;

    private float value;
    private float targetValue;

    private void Update()
    {
        if (value < targetValue)
            value = Math.Min(value + adaptability * Time.deltaTime, targetValue);
        if (value > targetValue)
            value = Math.Max(value - adaptability * Time.deltaTime, targetValue);

        renderer.material.SetColor("_BarColor",
            value <= 0.25f ?
                Color.red :
            value <= 0.5f ?
                Color.yellow :
                Color.green);

        renderer.material.SetFloat("_Cutoff", Mathf.Lerp(0.75f, 0.25f, value));
    }

    protected override void SetValue(float value)
    {
        targetValue = value;
    }
}
