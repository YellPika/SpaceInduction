using UnityEngine;

[RequireComponent(typeof(NoiseEffect))]
public sealed class PowerNoiseLink : Link<PowerProperty, float>
{
    private NoiseEffect noise;

    [SerializeField]
    private AnimationCurve curve;

    private void Awake()
    {
        noise = GetComponent<NoiseEffect>();
    }

    protected override void SetValue(float value)
    {
        noise.Opacity = curve.Evaluate(value);
    }
}
