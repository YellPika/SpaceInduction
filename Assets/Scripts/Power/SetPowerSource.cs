using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class SetPowerSource : PowerSource
{
    [SerializeField]
    private float power = 1;

    private List<PowerProperty> targets = new List<PowerProperty>();
    public List<PowerProperty> Targets { get { return targets; } }

    private void Update()
    {
        foreach (var target in targets)
            target.Apply(power);
    }
}
