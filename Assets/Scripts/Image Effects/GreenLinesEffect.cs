using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public sealed class GreenLinesEffect : MonoBehaviour
{
    [SerializeField]
    private Shader shader;
    private Texture2D noiseTexture;

    private Material material;
    private float offset;

    public float Period = 10;
    public float Speed = 10;
    public Color Color = new Color(0, 0.5f, 0);

    public float NoiseIntensity = 0.05f;
    public float LineIntensity = 0.1f;

    private void Awake()
    {
        var data = Enumerable
            .Range(0, 512)
            .Select(n => (byte)n)
            .ToArray();
        
        for (int i = 0; i < data.Length; i++)
        {
            var index = Random.Range(i, data.Length - 1);

            var temp = data[index];
            data[index] = data[i];
            data[i] = temp;
        }

        noiseTexture = new Texture2D(data.Length, 1, TextureFormat.ARGB32, false, true);
        noiseTexture.SetPixels32(data
            .Select(n => new Color32(n, n, n, n))
            .ToArray());
        noiseTexture.Apply();

        noiseTexture.filterMode = FilterMode.Point;
        noiseTexture.wrapMode = TextureWrapMode.Repeat;

        material = new Material(shader);
    }

    private void Update()
    {
        offset += Speed * Time.deltaTime;

        material.SetFloat("_Offset", offset);
        material.SetFloat("_Period", Period);
        material.SetColor("_Color", Color);
        material.SetFloat("_LineIntensity", LineIntensity);
        material.SetFloat("_NoiseIntensity", NoiseIntensity);
        material.SetTexture("_NoiseTex", noiseTexture);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
