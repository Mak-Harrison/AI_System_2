using UnityEngine;
using UnityEngine.AI;

public class GatheringAI : MonoBehaviour
{
    public enum AIState { Idle, Search, Fetch, Deliver, ReturningHome }
    public AIState currentState = AIState.Idle;

    private NavMeshAgent agent;
    private GameObject targetedResource;

    [Header("Target Locations")]
    public Transform bakeryPoint;     // Where Eggs and Gloomroot go
    public Transform gloomFieldPoint; // Where Souls go
    public Transform startPoint;      // Home base

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = AIState.ReturningHome;
    }

    void Update()
    {
        switch (currentState)
        {
            case AIState.Idle: ScanForResources(); break;
            case AIState.Fetch: MoveToResource(); break;
            case AIState.Deliver: DeliverResource(); break; // Combined logic here
            case AIState.ReturningHome: GoHome(); break;
        }
    }

    void ScanForResources()
    {
        // Search for all 3 types
        GameObject target = GameObject.FindGameObjectWithTag("Soul");
        if (target == null) target = GameObject.FindGameObjectWithTag("Gloomroot");
        if (target == null) target = GameObject.FindGameObjectWithTag("Egg");

        if (target != null)
        {
            targetedResource = target;
            currentState = AIState.Fetch;
        }
    }

    void MoveToResource()
    {
        if (targetedResource == null) { currentState = AIState.Idle; return; }

        agent.SetDestination(targetedResource.transform.position);

        if (Vector3.Distance(transform.position, targetedResource.transform.position) < 1.2f)
        {
            targetedResource.transform.parent = this.transform;
            targetedResource.transform.localPosition = new Vector3(0, 1, 1);
            currentState = AIState.Deliver;
        }
    }

    void DeliverResource()
    {
        // 1. Determine the correct destination based on the item tag
        Transform currentDestination = bakeryPoint;

        if (targetedResource.CompareTag("Soul"))
        {
            currentDestination = gloomFieldPoint;
        }

        // 2. Move to that destination
        agent.SetDestination(currentDestination.position);

        // 3. Once arrived, drop off and set the global bools
        if (agent.remainingDistance < 1f && !agent.pathPending)
        {
            if (targetedResource.CompareTag("Soul")) Collectable.hasSoul = true;
            if (targetedResource.CompareTag("Gloomroot")) Collectable.hasGloomroot = true;
            if (targetedResource.CompareTag("Egg")) Collectable.hasEgg = true;

            Destroy(targetedResource);
            currentState = AIState.ReturningHome;
        }
    }

    void GoHome()
    {
        agent.SetDestination(startPoint.position);
        if (agent.remainingDistance < 1f && !agent.pathPending)
        {
            currentState = AIState.Idle;
        }
    }
}
