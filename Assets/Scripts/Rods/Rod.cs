using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class Rod : MonoBehaviour
{
    private void OnTriggerEnter(Collider sender)
    {
        var inventory = sender.GetComponent<RodInventory>();
        if (inventory == null)
            return;

        renderer.enabled = false;
        collider.enabled = false;

        inventory.Items.Add(this);
    }

    private void Restart()
    {
        renderer.enabled = true;
        collider.enabled = true;
    }
}
