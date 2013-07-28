using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GlobBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform[] targets;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float turnSpeed;

    [SerializeField]
    private float tolerance;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private IEnumerator<Vector3> travelPath;

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void Start()
    {
        travelPath = TravelPath();
    }

    private void Restart()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        Start();
    }

    private void Update()
    {
        travelPath.MoveNext();

        var source = transform.rotation.eulerAngles;
        var target = Quaternion.LookRotation(travelPath.Current - transform.position).eulerAngles;

        transform.rotation = Quaternion.Euler(
            Mathf.MoveTowardsAngle(source.x, target.x, turnSpeed * Time.deltaTime),
            Mathf.MoveTowardsAngle(source.y, target.y, turnSpeed * Time.deltaTime),
            Mathf.MoveTowardsAngle(source.z, target.z, turnSpeed * Time.deltaTime));
    }

    private IEnumerator<Vector3> TravelPath()
    {
        var currentTarget = 0;
        var path = new NavMeshPath();
        var corners = new Queue<Vector3>();

        while (true)
        {
            if (corners.Count == 0)
            {
                currentTarget = (currentTarget + 1) % targets.Length;
                NavMesh.CalculatePath(transform.position, targets[currentTarget].transform.position, -1, path);

                foreach (var corner in path.corners)
                    corners.Enqueue(corner);
            }

            var target = corners.Peek();

            var direction = target - transform.position;
            var distance = direction.magnitude;

            if (distance < speed * Time.deltaTime)
                transform.position = target;
            else
            {
                transform.position += direction.normalized * speed * Time.deltaTime;
                yield return target;
            }

            if (Vector3.Distance(transform.position, target) < tolerance)
                corners.Dequeue();
        }
    }
}
