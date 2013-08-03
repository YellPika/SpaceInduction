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

        foreach (var child in transform.OfType<Transform>())
            if (child.name != "Entrance" && child.name != "Exit")
                child.gameObject.SetActive(false);

        entrance.Opened += (sender, e) =>
        {
            foreach (var child in transform.OfType<Transform>())
                if (child.name != "Entrance" && child.name != "Exit")
                    child.gameObject.SetActive(true);
        };

        exit.Closed += (sender, e) =>
        {
            // Make sure that we're not just restarting the level.
            if (entrance.IsOpen)
                return;

            foreach (var child in transform.OfType<Transform>())
                if (child.name != "Entrance" && child.name != "Exit")
                    child.gameObject.SetActive(false);
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
        foreach (var child in transform.OfType<Transform>())
            child.BroadcastMessage("Restart", SendMessageOptions.DontRequireReceiver);

        // TODO: Get rid of the code duplication.
        foreach (var child in transform.OfType<Transform>())
            if (child.name != "Entrance" && child.name != "Exit")
                child.gameObject.SetActive(false);
    }
}
