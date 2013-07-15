using UnityEngine;

// Closes a door when an object passes from one side of the door to the other.
// Does not open the door.
[RequireComponent(typeof(Collider))]
public sealed class PassThroughDoorTrigger : MonoBehaviour
{
    [SerializeField]
    private DoorBehaviour target;

    // Kinda abstract. Don't mess with it.
    private int state;

    private void Awake()
    {
        PassThroughDoorTriggerPart last = null;

        foreach (var trigger in GetComponentsInChildren<PassThroughDoorTriggerPart>())
        {
            var copy = trigger;

            trigger.Enter += (sender, e) =>
            {
                switch (state)
                {
                    case 0:
                        state = 1;
                        break;
                    case 1:
                    case 3:
                        state = 2;
                        break;
                }

                last = copy;
            };

            trigger.Exit += (sender, e) =>
            {
                switch (state)
                {
                    case 1:
                        state = 0;
                        break;
                    case 2:
                        state = sender == last ? 1 : 3;
                        break;
                    case 3:
                        state = 0;
                        target.Close();
                        break;
                }

                last = copy;
            };
        }
    }
}
