using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class Rod : MonoBehaviour
{
    [SerializeField]
    private AudioClip pickup;
    private AudioSource pickupSource;

    [SerializeField]
    private int index;

    private void Awake()
    {
        pickupSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider sender)
    {
        var inventory = sender.GetComponent<RodInventory>();
        if (inventory == null)
            return;

        //// Uncomment to enable ordered rods.
        //if (inventory.Total != index)
        //    return;

        pickupSource.PlayOneShot(pickup);

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

#if UNITY_EDITOR
    private void Reset()
    {
        var otherRods = FindSceneObjectsOfType(typeof(Rod))
            .OfType<Rod>()
            .Where(n => n != this)
            .ToArray();

        index = 0;
        while (otherRods.Any(n => n.index == index))
            index++;
    }
#endif
}
