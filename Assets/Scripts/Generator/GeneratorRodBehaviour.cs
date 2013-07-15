using UnityEngine;

public sealed class GeneratorRodBehaviour : MonoBehaviour
{
    private void Start() { Remove(); }
    public void Insert() { gameObject.SetActive(true); }
    public void Remove() { gameObject.SetActive(false); }
}
