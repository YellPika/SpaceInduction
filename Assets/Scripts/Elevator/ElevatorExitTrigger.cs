using System;
using UnityEngine;

// Fires an event when the elevator lift is exited.
public sealed class ElevatorExitTrigger : MonoBehaviour
{
    public EventHandler Triggered;

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<ElevatorOperator>() != null && Triggered != null)
            Triggered(this, EventArgs.Empty);
    }
}
