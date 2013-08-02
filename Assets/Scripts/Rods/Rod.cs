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
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);

        inventory.Items.Add(this);
    }

    private void Restart()
    {
        // Restart message won't be sent if we just gameObject.SetActive(false);
        // Have to do it the long way instead.
        renderer.enabled = true;
        collider.enabled = true;
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
    }
}
