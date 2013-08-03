using System.Collections.Generic;
using UnityEngine;

public sealed class RodInventory : MonoBehaviour
{
    private int total;
    private int count;

    public int Total { get { return total; } }

    public void Add()
    {
        count++;
        total++;
    }

    public int Remove()
    {
        var output = count;
        count = 0;

        if (total == 4)
            total = 0;

        return output;
    }

    private void Restart()
    {
        total = 0;
        count = 0;
    }
}
