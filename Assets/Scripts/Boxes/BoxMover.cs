using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class BoxMover : MonoBehaviour
{
    public bool IsActivated { get; set; }
}
