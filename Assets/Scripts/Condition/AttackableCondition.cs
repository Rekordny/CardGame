using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackableCondition : ConditionTask
{
    public BBParameter<Character> character;
    //public BBParameter<float> secondsToWait;
    protected override string OnInit()
    {
        return null;
    }

    protected override void OnEnable()
    {

    }

    protected override bool OnCheck()
    {
        return CharacterManager.Instance.IfEntityInAttackRange(character.value);
    }
}