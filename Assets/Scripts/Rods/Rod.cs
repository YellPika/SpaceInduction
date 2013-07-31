using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class Rod : MonoBehaviour
{
    private void OnTriggerEnter(Collider sender)
    {
        var inventory = sender.GetComponent<RodInventory>();
        if (inventory == null)
            return;

        gameObject.SetActive(false);
        inventory.Items.Add(this);
    }

    private void Restart()
    {
        gameObject.SetActive(true);
    }
}
