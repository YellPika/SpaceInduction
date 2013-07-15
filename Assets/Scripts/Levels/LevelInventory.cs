using UnityEngine;

public sealed class LevelInventory : MonoBehaviour
{
    [SerializeField]
    private Level current;

    public Level Current
    {
        get { return current; }
        set { current = value; }
    }
}
