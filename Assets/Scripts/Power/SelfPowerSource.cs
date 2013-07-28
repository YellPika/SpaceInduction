using UnityEngine;

[RequireComponent(typeof(PowerProperty))]
public sealed class SelfPowerSource : PowerSource
{
    [SerializeField]
    private float efficiency = 1;
    private PowerProperty power;

    public float Efficiency
    {
        get { return efficiency; }
        set { efficiency = value; }
    }

    private void Awake()
    {
        power = GetComponent<PowerProperty>();
    }

    private void Update()
    {
        power.Apply(power.Value - (1 - efficiency) * Time.deltaTime);
    }
}
