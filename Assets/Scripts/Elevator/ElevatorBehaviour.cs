using UnityEngine;

public enum ElevatorState { Up, Down }

// Logic for operating the elevator.
public sealed class ElevatorBehaviour : MonoBehaviour
{
    private bool isFull;

    [SerializeField]
    private ElevatorState state = ElevatorState.Down;
    public ElevatorState State { get { return state; } }

    private void Awake()
    {
        animation.Play("Elevator." + state);

        GetComponentInChildren<ElevatorEnterTrigger>().Triggered +=
            (sender, e) =>
            {
                // The player hasn't exited yet.
                if (isFull)
                    return;

                isFull = true;

                state = state == ElevatorState.Down
                    ? ElevatorState.Up
                    : ElevatorState.Down;

                animation.Play("Elevator." + state);
            };

        GetComponentInChildren<ElevatorExitTrigger>().Triggered +=
            (sender, e) =>
            {
                isFull = false;
            };
    }
}
