using UnityEngine;

[RequireComponent(typeof(NoiseEffect))]
[RequireComponent(typeof(AudioSource))]
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
        audio.volume = curve.Evaluate(value) * 10;
        noise.Opacity = curve.Evaluate(value);
    }
}
