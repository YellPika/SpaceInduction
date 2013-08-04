using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class ColliderPowerSource : PowerSource
{
    [SerializeField]
    private float power = 1;
    private HashSet<PowerProperty> targets = new HashSet<PowerProperty>();

    private void OnTriggerEnter(Collider collider)
    {
        var property = collider.GetComponent<PowerProperty>();
        if (property != null)
            targets.Add(property);
    }

    private void OnTriggerExit(Collider collider)
    {
        var property = collider.GetComponent<PowerProperty>();
        if (property != null)
            targets.Remove(property);
    }

    private void Update()
    {
        foreach (var target in targets)
            target.Apply(power);
    }

    private void Restart()
    {
        targets.Clear();
        collider.enabled = false;

        // This is a hack, because for some stupid reason, iterator coroutines won't work.
        Invoke("Reenable", 0);
    }

    private void Reenable()
    {
        collider.enabled = true;
    }
}
