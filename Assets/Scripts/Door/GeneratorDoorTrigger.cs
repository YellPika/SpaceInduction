using UnityEngine;

[RequireComponent(typeof(GeneratorBehaviour))]
public sealed class GeneratorDoorTrigger : MonoBehaviour
{
    [SerializeField]
    private DoorBehaviour target;

    private void Awake()
    {
        GetComponentInChildren<GeneratorBehaviour>().Started +=
            (sender, e) => target.Open();
    }
}
