using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class ProximityDoorTrigger : MonoBehaviour
{
    [SerializeField]
    private DoorBehaviour target;
    private int occupants = 0;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<DoorOpener>() == null)
            return;

        occupants++;
        target.Open();
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<DoorOpener>() == null)
            return;

        if (--occupants == 0)
            target.Close();
    }
}
