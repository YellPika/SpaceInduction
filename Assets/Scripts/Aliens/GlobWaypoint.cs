using UnityEngine;

public sealed class GlobWaypoint : MonoBehaviour
{
    [SerializeField]
    private float tolerance = 0.5f;

    public float Tolerance { get { return tolerance; } }
	
}