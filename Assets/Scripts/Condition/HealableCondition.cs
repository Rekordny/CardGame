using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealableCondition : ConditionTask
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
        return CharacterManager.Instance.IfEntityInHealRange(character.value);
    }
}
