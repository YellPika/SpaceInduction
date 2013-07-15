using System;
using UnityEngine;

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
