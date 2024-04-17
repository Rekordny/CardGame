using NodeCanvas.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class WanderAction : ActionTask
{
    private NavMeshAgent navAgent;
    
    public BBParameter<Character> character;
    public BBParameter<float> lookAheadDistance;
    public BBParameter<float> lookAheadRadius;


    // ==Awake
    protected override string OnInit()
    {
        // 尼玛的啥比不让我链接到外面，只能自己找reference
        //characterReference = animator.value.gameObject.GetComponent<Character>();
        //Debug.Log(characterReference.value.characterName);
        return base.OnInit();
    }

    protected override void OnExecute()
    {
        navAgent = character.value.GetComponent<NavMeshAgent>();
        // 在这设置agent speed
        // navAgent.value.speed = characterReference.value.MoveSpeed;
        character.value.SetTriggerAnimation(Utils.walkingParam);
    }

    protected override void OnUpdate()
    {
        //Debug.Log(characterReference.value.characterName);
        if (!navAgent.hasPath)
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
            navAgent.destination = hit.position;
        }

        Vector3 direction = navAgent.nextPosition - agent.transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(direction);

        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, desiredRotation, Time.deltaTime);

        if(Vector3.Distance(agent.transform.position, navAgent.destination)< 0.5f)
            navAgent.ResetPath();
    }

    protected override void OnStop()
    {
        if (navAgent.isOnNavMesh)
            navAgent.ResetPath();
        character.value.SetTriggerAnimation(Utils.idlingParam);
    }
}
