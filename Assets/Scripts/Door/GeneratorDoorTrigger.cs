using UnityEngine;

[RequireComponent(typeof(GeneratorBehaviour))]
public sealed class GeneratorDoorTrigger : MonoBehaviour
{
    [SerializeField]
    private DoorBehaviour target;

    private void Awake()
    {
        GetComponentInChildren<GeneratorTriggerBehaviour>().Triggered +=
            (sender, e) => target.Open();
    }
}
