using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public sealed class MovableBox : MonoBehaviour
{
    private Vector3 startPosition;

    [SerializeField]
    private AudioClip move;
    private AudioSource moveSource;

    private BoxMover mover;
    private Vector3 previousMoverPosition;

    private float volume;
    private float targetVolume;

    [SerializeField]
    private PhysicMaterial smoothMaterial;

    [SerializeField]
    private PhysicMaterial roughMaterial;

    private void Awake()
    {
        moveSource = gameObject.AddComponent<AudioSource>();
        moveSource.clip = move;
        moveSource.loop = true;
        moveSource.Play();

        startPosition = transform.position;
    }

    private void Update()
    {
        targetVolume = 0;

        rigidbody.mass = mover != null && mover.IsActivated ? 0.1f : 10;
        collider.material = mover != null && mover.IsActivated ? smoothMaterial : roughMaterial;

        if (mover != null)
        {
            if (mover.IsActivated)
            {
                var toMover = mover.transform.position - transform.position;
                
                if (Mathf.Abs(toMover.x) > Mathf.Abs(toMover.z))
                {
                    var offset = mover.transform.position.x - previousMoverPosition.x;
                    rigidbody.MovePosition(rigidbody.position + Vector3.right * offset);
                    rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

                    if (Mathf.Abs(offset) > 0.01f)
                        targetVolume += 1;
                }
                else
                {
                    var offset = mover.transform.position.z - previousMoverPosition.z;
                    rigidbody.MovePosition(rigidbody.position + Vector3.forward * offset);
                    rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
                    
                    if (Mathf.Abs(offset) > 0.01f)
                        targetVolume += 1;
                }
            }

            previousMoverPosition = mover.transform.position;
        }

        volume = Mathf.Clamp01(volume + Mathf.Sign(targetVolume - volume) * 4 * Time.deltaTime);
        moveSource.volume = Mathf.Pow(volume, 4);
    }

    private void OnTriggerEnter(Collider collider)
    {
        var mover = collider.GetComponent<BoxMover>();
        if (mover != null && renderer.isVisible)
        {
            this.mover = mover;
            previousMoverPosition = mover.transform.position;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        var mover = collider.GetComponent<BoxMover>();
        if (this.mover == mover)
            this.mover = null;
    }

    private void Restart()
    {
        transform.position = startPosition;
        mover = null;
    }
}
