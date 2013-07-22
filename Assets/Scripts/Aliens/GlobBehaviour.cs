using UnityEngine;

public sealed class GlobBehaviour : MonoBehaviour
{
    [SerializeField]
    private GlobWaypoint[] targets;
    private int currentTarget;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (targets == null || targets.Length == 0)
            return;

        currentTarget = 0;
        agent.SetDestination(targets[currentTarget].transform.position);
    }

    private void Update()
    {
        if (targets == null || targets.Length == 0)
            return;

        if (Vector3.Distance(transform.position, targets[currentTarget].transform.position) < targets[currentTarget].Tolerance)
        {
            currentTarget = (currentTarget + 1) % targets.Length;
            agent.SetDestination(targets[currentTarget].transform.position);
        }
    }
}
