using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class AlienPowerSource : PowerSource
{
    [SerializeField]
    private float power = 0.75f;
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
        {
            if (target.GetComponent<PlayerBehaviour>() != null)
                target.Apply(power * Time.deltaTime * -0.5f);
            else
                target.Apply(-power);
        }
    }
}
