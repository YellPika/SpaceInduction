using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PowerProperty))]
public abstract class PowerSource : MonoBehaviour
{
    [SerializeField]
    private float efficiency = 1;

    public PowerProperty Power { get; private set; }

    private void Awake()
    {
        Power = GetComponent<PowerProperty>();
    }

    private void Update()
    {
        foreach (var target in GetTargets())
            target.Apply(Power.Value - (1 - efficiency) * Time.deltaTime);
    }

    protected abstract IEnumerable<PowerProperty> GetTargets();
}
