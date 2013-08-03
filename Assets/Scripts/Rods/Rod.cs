using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class Rod : MonoBehaviour
{
    [SerializeField]
    private int index;

    private void OnTriggerEnter(Collider sender)
    {
        var inventory = sender.GetComponent<RodInventory>();
        if (inventory == null || inventory.Total != index)
            return;

        inventory.Add();
        renderer.enabled = false;
        collider.enabled = false;
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
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
