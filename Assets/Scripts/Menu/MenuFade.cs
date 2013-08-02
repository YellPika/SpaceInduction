using System;
using UnityEngine;

[RequireComponent(typeof(GUITexture))]
public sealed class MenuFade : MonoBehaviour
{
    private Color baseColor;
    private float alpha;
    private float targetAlpha;

    private float previousTime;

    [SerializeField]
    private float fadeSpeed = 1;

    private void Awake()
    {
        baseColor = guiTexture.color;
    }

    private void Update()
    {
        var currentTime = Time.realtimeSinceStartup;

        alpha = Mathf.Clamp01(alpha + Math.Sign(targetAlpha - alpha) * fadeSpeed * (currentTime - previousTime));
        guiTexture.color = baseColor * alpha;

        previousTime = currentTime;
    }

    private void Enable() { targetAlpha = 1; }
    private void Disable() { targetAlpha = 0; }
}
