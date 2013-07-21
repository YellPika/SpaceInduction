using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public sealed class GreenLinesEffect : MonoBehaviour
{
    [SerializeField]
    private Shader shader;
    private Material material;
    private float offset;

    public float Period = 10;
    public float Speed = 10;
    public float Intensity = 0.5f;
    public Color Color = new Color(0, 0.5f, 0);

    private void Awake()
    {
        material = new Material(shader);
    }

    private void Update()
    {
        offset += Speed * Time.deltaTime;
        material.SetFloat("_Offset", offset);
        material.SetFloat("_Period", Period);
        material.SetFloat("_Intensity", Intensity);
        material.SetColor("_Color", Color);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
