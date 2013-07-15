using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class OpenDoorTrigger : MonoBehaviour
{
    [SerializeField]
    private DoorBehaviour target;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<DoorOpener>() == null)
            return;

        target.Open();
    }
}
