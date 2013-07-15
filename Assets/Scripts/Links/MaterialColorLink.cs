using UnityEngine;

[RequireComponent(typeof(Renderer))]
public sealed class MaterialColorLink : Link<ColorProperty, Color>
{
    private Material material;

    [SerializeField]
    private int index;

    [SerializeField]
    private string property = "_FloorEmitColor";

    private void Awake()
    {
        material = renderer.materials[index];
    }

    protected override void SetValue(Color value)
    {
        material.SetColor(property, value);
    }
}
