using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public sealed class InterlaceEffect : MonoBehaviour
{
    public Shader Shader;
    private Material material;

    public float Period = 4;
    public float Speed = 10;

    public Color Color = new Color(0, 0.5f, 0);
    public float Intensity = 0.1f;

    private void Awake()
    {
        material = new Material(Shader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_Period", Period);
        material.SetFloat("_Speed", Speed);
        material.SetColor("_Color", Color);
        material.SetFloat("_Intensity", Intensity);

        Graphics.Blit(source, destination, material);
    }
}
