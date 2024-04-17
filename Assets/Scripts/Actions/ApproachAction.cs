using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ApproachAction : ActionTask
{
    public BBParameter<Character> character;

    public bool isEnemy;

    // Awake
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
        if(isEnemy)
            character.value.MoveTo(CharacterManager.Instance.GetClosestEnemy(character.value).transform.position);
        else
            character.value.MoveTo(CharacterManager.Instance.GetClosestHealableAlly(character.value).transform.position);
        if (Vector3.Distance(agent.transform.position, character.value.GetComponent<NavMeshAgent>().destination) < character.value.AttackRange)
        {
            character.value.GetComponent<NavMeshAgent>().ResetPath();
            EndAction(true);
        }
    }

    protected override void OnStop()
    {
        if (character.value.GetComponent<NavMeshAgent>().isOnNavMesh)
            character.value.GetComponent<NavMeshAgent>().ResetPath();
        character.value.SetTriggerAnimation(Utils.idlingParam);
        
    }
}
