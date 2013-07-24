using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class BoxMover : MonoBehaviour
{
    private Vector3 startPosition;

    public bool IsActivated { get; set; }

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void OnTriggerStay(Collider collider)
    {
        var box = collider.GetComponent<MovableBox>();
        if (box == null)
            return;

        if (IsActivated)
        {
            var offset = box.transform.position - transform.position;

            if (Mathf.Abs(offset.x) > Mathf.Abs(offset.z))
                box.rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, 0);
            else
                box.rigidbody.velocity = new Vector3(0, 0, rigidbody.velocity.z);
        }
    }

    private void Restart()
    {
        transform.position = startPosition;
    }
}
