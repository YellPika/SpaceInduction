using System.Collections.Generic;

public sealed class SelfPowerSource : PowerSource
{
    protected override IEnumerable<PowerProperty> GetTargets()
    {
        yield return Power;
    }
}
