using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class RespawnPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        var inventory = collider.GetComponent<RespawnPointInventory>();
        if (inventory != null)
            inventory.Current = this;
    }
}
