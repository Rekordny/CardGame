using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityExistCondition : ConditionTask
{
    public BBParameter<Character> character;
    public bool IsEnemy;
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
        return IsEnemy?CharacterManager.Instance.IfEnemyExist(character.value): CharacterManager.Instance.IfAllyExist(character.value);
    }
}

