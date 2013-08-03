using UnityEngine;

public sealed class GeneratorRodBehaviour : MonoBehaviour
{
    private void Start() { Remove(); }
    private void Restart() { Remove(); }

    public void Insert() { renderer.enabled = true; }
    public void Remove() { renderer.enabled = false; }
}
