using System;
using UnityEngine;

// Helper component for the PassThroughDoorTrigger,
// to which this passes trigger messages to.
[RequireComponent(typeof(Collider))]
public sealed class PassThroughDoorTriggerPart : MonoBehaviour
{
    public event EventHandler Enter;
    public event EventHandler Exit;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<DoorOpener>() == null)
            return;

        if (Enter != null)
            Enter(this, EventArgs.Empty);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<DoorOpener>() == null)
            return;

        if (Exit != null)
            Exit(this, EventArgs.Empty);
    }
}
