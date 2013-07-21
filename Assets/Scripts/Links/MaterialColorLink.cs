using UnityEngine;

[RequireComponent(typeof(Renderer))]
public sealed class MaterialColorLink : Link<ColorProperty, Color>
{
    [SerializeField]
    private int index;

    [SerializeField]
    private string property = "_FloorEmitColor";

    protected override void SetValue(Color value)
    {
        renderer.materials[index].SetColor(property, value);
    }
}
