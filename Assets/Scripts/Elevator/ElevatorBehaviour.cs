using UnityEngine;

public enum ElevatorState { Up, Down }

// Logic for operating the elevator.
public sealed class ElevatorBehaviour : MonoBehaviour
{
    private ElevatorState initialState;
    private bool isFull;

    [SerializeField]
    private ElevatorState state = ElevatorState.Down;
    public ElevatorState State { get { return state; } }

    private void Awake()
    {
        initialState = state;

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

    private void Restart()
    {
        state = initialState;
        animation.Play("Elevator." + state);
    }
}
