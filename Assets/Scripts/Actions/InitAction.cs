using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InitAction : ActionTask
{
    
    public BBParameter<Character> character;
    private NavMeshAgent navAgent;
    protected override void OnExecute()
    {
        navAgent = character.value.GetComponent<NavMeshAgent>();
        //设定速度等

    }

    protected override void OnUpdate()
    {

    }

    protected override void OnStop()
    {

    }
}