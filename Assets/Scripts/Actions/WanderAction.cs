using NodeCanvas.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class WanderAction : ActionTask
{
    public BBParameter<NavMeshAgent> navAgent;
    public BBParameter<Animator> animator;
    public BBParameter<float> moveSpeed;
    public BBParameter<float> lookAheadDistance;
    public BBParameter<float> lookAheadRadius;

    private readonly int walkinParam = Animator.StringToHash("Walking");

    // ==Awake
    protected override string OnInit()
    {
        return base.OnInit();
    }

    protected override void OnExecute()
    {
        base.OnExecute();
    }

    protected override void OnUpdate()
    {
        if (!navAgent.value.hasPath)
        {
            Vector3 wanderPoint = Vector3.zero;
            NavMeshHit hit;
            do
            {
                Vector3 lookAheadPoint = agent.transform.position + agent.transform.forward * lookAheadDistance.value;
                Vector3 randomPoint = Random.insideUnitSphere * lookAheadRadius.value;
                wanderPoint = lookAheadPoint + randomPoint;
            }
            while (!NavMesh.SamplePosition(wanderPoint, out hit, lookAheadRadius.value + 1f, NavMesh.AllAreas));
            navAgent.value.destination = hit.position;
        }

        Vector3 direction = navAgent.value.nextPosition - agent.transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(direction);

        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, desiredRotation, Time.deltaTime);

        if(Vector3.Distance(agent.transform.position, navAgent.value.destination)< 0.5f)
            navAgent.value.ResetPath();
    }

    protected override void OnStop()
    {
        if (navAgent.value.isOnNavMesh)
            navAgent.value.ResetPath();
        animator.value.SetBool(walkinParam, false);
    }
}
