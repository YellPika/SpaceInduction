using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class ColliderPowerSource : PowerSource
{
    private List<PowerProperty> targets = new List<PowerProperty>();

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

    protected override IEnumerable<PowerProperty> GetTargets()
    {
        foreach (var target in targets)
            yield return target;
    }
}
