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

    private void Start()
    {
        gameObject.SampleAnimation(animation.GetClip("Elevator." + (state == ElevatorState.Down ? "Up" : "Down")), 0);
    }

    private void Restart()
    {
        state = initialState;
        Start();
    }
}
