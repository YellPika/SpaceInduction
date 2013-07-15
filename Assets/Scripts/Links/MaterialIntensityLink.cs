using UnityEngine;

[RequireComponent(typeof(Renderer))]
public sealed class MaterialIntensityLink : Link<IntensityProperty, float>
{
    private float baseIntensity;
    private Material material;

    [SerializeField]
    private int index;

    [SerializeField]
    private string property = "_Intensity";

    private void Awake()
    {
        material = renderer.materials[index];
        baseIntensity = material.GetFloat(property);
    }

    protected override void SetValue(float value)
    {
        material.SetFloat(property, baseIntensity * value);
    }
}
