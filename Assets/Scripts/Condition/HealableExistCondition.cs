using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class HealableExistCondition : ConditionTask
{
    public BBParameter<Character> character;

    protected override bool OnCheck()
    {
        return CharacterManager.Instance.IfHealableAllyExist(character.value);
    }
}
