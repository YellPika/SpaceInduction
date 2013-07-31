using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public sealed class MovableBox : MonoBehaviour
{
    private Vector3 startPosition;

    [SerializeField]
    private PhysicMaterial smoothMaterial;

    [SerializeField]
    private PhysicMaterial roughMaterial;
    
    private void Awake()
    {
        startPosition = transform.position;
    }

    private void OnTriggerStay(Collider collider)
    {
        var mover = collider.GetComponent<BoxMover>();
        if (mover == null || !renderer.isVisible)
            return;

        rigidbody.mass = mover.IsActivated ? 0.1f : 10;
        collider.material = mover.IsActivated ? smoothMaterial : roughMaterial;
		
        if (mover.IsActivated)
        {
			var offset = mover.transform.position - transform.position;

            if (Mathf.Abs(offset.x) > Mathf.Abs(offset.z))
            {
                rigidbody.velocity = new Vector3(mover.rigidbody.velocity.x, 0, 0);
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            }
            else
            {
                rigidbody.velocity = new Vector3(0, 0, mover.rigidbody.velocity.z);
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
            }

            // to prevent the box from moving slower than the player.
            rigidbody.velocity *= 1.25f;
        }
    }

    private void Restart()
    {
        transform.position = startPosition;
    }
}
