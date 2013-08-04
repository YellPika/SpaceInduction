using System;
using UnityEngine;

public sealed class GeneratorTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioClip insert;
    private AudioSource insertSource;

    private GeneratorRodBehaviour[] rods;
    private int insertionCount = 0;

    public int InsertionCount { get { return insertionCount; } }
    public int RodCount { get { return rods.Length; } }

    public event EventHandler Triggered;

    private void Awake()
    {
        insertSource = gameObject.AddComponent<AudioSource>();

        rods = GetComponentsInChildren<GeneratorRodBehaviour>();
    }

    private void Restart()
    {
        foreach (var rod in rods)
            rod.Remove();

        insertionCount = 0;
    }

    private void OnTriggerEnter(Collider collider)
    {
        var inventory = collider.GetComponent<RodInventory>();
        if (inventory == null)
            return;

        var count = inventory.Remove();
        if (count == 0)
            return;

        insertSource.PlayOneShot(insert);

        for (int i = 0; i < count; i++)
            rods[insertionCount++].Insert();

        if (Triggered != null)
            Triggered(this, EventArgs.Empty);
    }
}
