using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public sealed class PowerRadiusLink : Link<PowerProperty, float>
{
    private SphereCollider sphere;
    private float baseRadius;

    private void Awake()
    {
        sphere = GetComponent<SphereCollider>();
        baseRadius = sphere.radius;
    }

    protected override void SetValue(float value)
    {
        sphere.radius = value * baseRadius;
    }
}
