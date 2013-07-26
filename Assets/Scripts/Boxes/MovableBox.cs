using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public sealed class MovableBox : MonoBehaviour
{
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void OnTriggerStay(Collider collider)
    {
        var mover = collider.GetComponent<BoxMover>();
        if (mover == null)
            return;

        if (mover.IsActivated)
        {
            var offset = mover.transform.position - transform.position;

            if (Mathf.Abs(offset.x) > Mathf.Abs(offset.z))
                rigidbody.velocity = new Vector3(mover.rigidbody.velocity.x, 0, 0);
            else
                rigidbody.velocity = new Vector3(0, 0, mover.rigidbody.velocity.z);
        }
    }

    private void Restart()
    {
        transform.position = startPosition;
    }
}
