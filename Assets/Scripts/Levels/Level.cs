using System.Linq;
using UnityEngine;

public sealed class Level : MonoBehaviour
{
    [SerializeField]
    private float timeLimit = 60;

    public float TimeLimit { get { return timeLimit; } }

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
                if (child.name != "Entrance")
                    child.gameObject.SetActive(true);
        };

        exit.Closed += (sender, e) =>
        {
            foreach (var child in transform.OfType<Transform>())
                if (child.name != "Exit")
                    child.gameObject.SetActive(false);
        };
    }

    private void OnTriggerEnter(Collider collider)
    {
        var inventory = collider.GetComponent<LevelInventory>();
        if (inventory == null || inventory.Current == this)
            return;

        inventory.Current = this;
        inventory.gameObject.transform.parent = transform;
    }

    public void Restart()
    {
        foreach (var child in transform.OfType<Transform>())
            child.BroadcastMessage("Restart", SendMessageOptions.DontRequireReceiver);
    }
}
