using System.Linq;
using UnityEngine;

public sealed class Level : MonoBehaviour
{
    public void Restart()
    {
        foreach (var child in transform.OfType<Transform>())
            child.BroadcastMessage("Restart", SendMessageOptions.DontRequireReceiver);
    }

    private void OnTriggerEnter(Collider collider)
    {
        var inventory = collider.GetComponent<LevelInventory>();
        if (inventory == null)
            return;

        inventory.Current = this;
        inventory.gameObject.transform.parent = transform;
    }
}
