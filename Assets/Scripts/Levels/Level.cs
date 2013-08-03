using System.Linq;
using UnityEngine;

public sealed class Level : MonoBehaviour
{
    [SerializeField]
    private float timeLimit = 60;

    [SerializeField]
    private int number;

    public float TimeLimit { get { return timeLimit; } }
    public int Number { get { return number; } }

    private void Start()
    {
        var entrance = transform.Find("Entrance").GetComponentInChildren<DoorBehaviour>();
        var exit = transform.Find("Exit").GetComponentInChildren<DoorBehaviour>();

        SetVisibility(false);

        entrance.Opened += (sender, e) => SetVisibility(true);
        exit.Closed += (sender, e) =>
        {
            // Make sure that we're not just restarting the level.
            if (!entrance.IsOpen)
                SetVisibility(false);
        };
    }

    private void OnTriggerEnter(Collider collider)
    {
        var inventory = collider.GetComponent<LevelInventory>();
        if (inventory == null || inventory.Current == this)
            return;

        inventory.Current = this;
    }

    public void Restart()
    {
        SetVisibility(true);

        foreach (var child in transform.OfType<Transform>())
            child.BroadcastMessage("Restart", SendMessageOptions.DontRequireReceiver);

        SetVisibility(false);
    }

    private void SetVisibility(bool value)
    {
        foreach (var child in transform.OfType<Transform>())
        {
            if (child.name == "Entrance" || child.name == "Exit")
                continue;

            child.gameObject.SetActive(value);
        }
    }
}
