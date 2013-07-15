using System;
using UnityEngine;

public sealed class RespawnPointInventory : MonoBehaviour
{
    [SerializeField]
    private RespawnPoint current;

    public RespawnPoint Current
    {
        get { return current; }
        set { current = value; }
    }
}
