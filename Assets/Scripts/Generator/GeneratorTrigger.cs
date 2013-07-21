using System;
using UnityEngine;

public sealed class GeneratorTrigger : MonoBehaviour
{
    private GeneratorRodBehaviour[] rods;
    private int insertionCount = 0;

    public int InsertionCount { get { return insertionCount; } }
    public int RodCount { get { return rods.Length; } }

    public event EventHandler Triggered;

    private void Awake()
    {
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
        if (inventory == null || inventory.Items.Count == 0)
            return;

        inventory.Items.RemoveAll(_ =>
            {
                if (insertionCount >= rods.Length)
                    return false;

                rods[insertionCount++].Insert();
                return true;
            });

        if (Triggered != null)
            Triggered(this, EventArgs.Empty);
    }
}
