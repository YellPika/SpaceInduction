using UnityEngine;

// Closes a door when an object passes from one side of the door to the other.
// Does not open the door.
public sealed class PassThroughDoorTrigger : MonoBehaviour
{
    [SerializeField]
    private DoorBehaviour target;

    // Kinda abstract. Don't mess with it.
    private int state;

    private void Awake()
    {
        object first = null;

        foreach (var trigger in GetComponentsInChildren<PassThroughDoorTriggerPart>())
        {
            trigger.Enter += (sender, e) =>
            {
                switch (state)
                {
                    case 0:
                        state = 1;
                        first = sender;
                        break;
                    case 1:
                    case 3:
                        state = 2;
                        break;
                }
            };

            trigger.Exit += (sender, e) =>
            {
                switch (state)
                {
                    case 1:
                        state = 0;
                        break;
                    case 2:
                        state = sender == first ? 3 : 1;
                        break;
                    case 3:
                        state = 0;
                        target.Close();
                        break;
                }
            };
        }
    }

    private void Restart()
    {
        state = 0;
    }
}
