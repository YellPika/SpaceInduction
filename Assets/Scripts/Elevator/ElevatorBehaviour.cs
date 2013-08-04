using UnityEngine;

public enum ElevatorState { Up, Down }

// Logic for operating the elevator.
public sealed class ElevatorBehaviour : MonoBehaviour
{
    [SerializeField]
    private AudioClip moveUp;
    [SerializeField]
    private AudioClip moveDown;
    [SerializeField]
    private AudioClip slide;

    private AudioSource moveSource;
    private AudioSource slideSource;

    private ElevatorState initialState;
    private bool isFull;

    [SerializeField]
    private ElevatorState state = ElevatorState.Down;
    public ElevatorState State { get { return state; } }

    private void Awake()
    {
        initialState = state;

        moveSource = transform.FindChild("Lift").gameObject.AddComponent<AudioSource>();
        slideSource = transform.FindChild("Lift").gameObject.AddComponent<AudioSource>();

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

                moveSource.mute = false;
                moveSource.PlayOneShot(state == ElevatorState.Up ? moveUp : moveDown);

                slideSource.PlayOneShot(slide);
            };

        GetComponentInChildren<ElevatorExitTrigger>().Triggered +=
            (sender, e) =>
            {
                isFull = false;
            };
    }

    private void Update()
    {
        moveSource.pitch = Time.timeScale;
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

    private void OnOpen()
    {
        slideSource.PlayOneShot(slide);
    }
}
