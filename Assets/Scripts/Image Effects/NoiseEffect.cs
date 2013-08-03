using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public sealed class NoiseEffect : MonoBehaviour
{
    public Shader Shader;
    private Material material;
    private Texture2D noise;

    public Color Color = Color.white;
    public float Intensity = 0.5f;
    public float Opacity = 0.1f;

    private void Start()
    {
        if (noise == null)
        {
            var data = Enumerable
                .Range(0, 256)
                .Select(n => (byte)n)
                .Select(n => new Color32(n, n, n, n))
                .ToArray();
            for (int i = 0; i < data.Length; i++)
            {
                var index = Random.Range(i, data.Length);
                var temp = data[index];
                data[index] = data[i];
                data[i] = temp;
            }

            noise = new Texture2D(data.Length, 1, TextureFormat.ARGB32, false, true);
            noise.hideFlags = HideFlags.DontSave;
            noise.filterMode = FilterMode.Bilinear;
            noise.wrapMode = TextureWrapMode.Repeat;
            noise.SetPixels32(data);
            noise.Apply();
        }

        if (material == null)
        {
            material = new Material(Shader);
            material.hideFlags = HideFlags.DontSave;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetColor("_Color", Color);
        material.SetFloat("_Intensity", Intensity);
        material.SetFloat("_Opacity", Opacity);
        material.SetTexture("_NoiseTex", noise);

        Graphics.Blit(source, destination, material);
    }
}
