using UnityEngine;

[RequireComponent(typeof(SelfPowerSource))]
public sealed class LevelInventory : MonoBehaviour
{
    [SerializeField]
    private Level current;

    private SelfPowerSource source;

    public Level Current
    {
        get { return current; }
        set
        {
            current = value;
            source.Efficiency = 1 - 1 / current.TimeLimit;
        }
    }

    private void Awake()
    {
        source = GetComponent<SelfPowerSource>();
    }
}
