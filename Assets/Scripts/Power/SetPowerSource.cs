using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class SetPowerSource : PowerSource
{
    private List<PowerProperty> targets = new List<PowerProperty>();

    public List<PowerProperty> Targets { get { return targets; } }

    protected override IEnumerable<PowerProperty> GetTargets()
    {
        return targets;
    }
}
