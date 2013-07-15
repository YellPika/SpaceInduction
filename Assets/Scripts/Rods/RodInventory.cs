using System.Collections.Generic;
using UnityEngine;

public sealed class RodInventory : MonoBehaviour
{
    private List<Rod> items = new List<Rod>();

    public List<Rod> Items { get { return items; } }

    private void Restart()
    {
        items.Clear();
    }
}
