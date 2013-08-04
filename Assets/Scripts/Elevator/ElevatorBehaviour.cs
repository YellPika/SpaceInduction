using UnityEngine;

public enum ElevatorState { Up, Down }

// Logic for operating the elevator.
public sealed class ElevatorBehaviour : MonoBehaviour
{
    [SerializeField]
    private AudioClip moveUp;
    [SerializeField]
    private AudioClip moveDown;

    private AudioSource moveSource;

    private ElevatorState initialState;
    private bool isFull;

    [SerializeField]
    private ElevatorState state = ElevatorState.Down;
    public ElevatorState State { get { return state; } }

    private void Awake()
    {
        initialState = state;

        moveSource = gameObject.AddComponent<AudioSource>();

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

                moveSource.clip = state == ElevatorState.Up ? moveUp : moveDown;
                moveSource.Play();
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
