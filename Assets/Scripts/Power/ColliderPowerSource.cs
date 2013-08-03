﻿using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class ColliderPowerSource : PowerSource
{
    [SerializeField]
    private float power = 1;
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

    private void Update()
    {
        foreach (var target in targets)
            target.Apply(power);
    }

    private void Restart()
    {
        targets.Clear();
    }
}
