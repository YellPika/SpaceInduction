using UnityEngine;

// Opens a door when a collider is triggered.
// Does not close the door.
[RequireComponent(typeof(Collider))]
public sealed class OpenDoorTrigger : MonoBehaviour
{
    [SerializeField]
    private DoorBehaviour target;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<DoorOpener>() != null)
            target.Open();
    }
	
}
