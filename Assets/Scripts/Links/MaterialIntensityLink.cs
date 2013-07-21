using UnityEngine;

[RequireComponent(typeof(Renderer))]
public sealed class MaterialIntensityLink : Link<IntensityProperty, float>
{
    private float baseIntensity;

    [SerializeField]
    private int index;

    [SerializeField]
    private string property = "_Intensity";

    private void Awake()
    {
        baseIntensity = renderer.materials[index].GetFloat(property);
    }

    protected override void SetValue(float value)
    {
        renderer.materials[index].SetFloat(property, baseIntensity * value);
    }
}
