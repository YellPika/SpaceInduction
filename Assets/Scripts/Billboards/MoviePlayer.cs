using UnityEngine;

public sealed class MoviePlayer : MonoBehaviour
{
    private void Start()
    {
        var texture = renderer.material.mainTexture as MovieTexture;
        texture.loop = true;
        texture.Play();
    }
}
