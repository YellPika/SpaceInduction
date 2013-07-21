using System;
using UnityEngine;

// Fires an event when the elevator lift is entered.
public sealed class ElevatorEnterTrigger : MonoBehaviour
{
    public EventHandler Triggered;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<ElevatorOperator>() != null && Triggered != null)
            Triggered(this, EventArgs.Empty);
    }
}
