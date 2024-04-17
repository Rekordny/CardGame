using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class AttackAction : ActionTask
{
    public BBParameter<Character> character;
    private AnimatorStateInfo animatorStateInfo;
    // Awake
    protected override string OnInit()
    {
        return base.OnInit();
    }


    protected override void OnExecute()
    {
        if(character.value.characterData.attack>=0)
            character.value.AttackCharacter(CharacterManager.Instance.GetClosestEnemy(character.value));
        else
            character.value.AttackCharacter(CharacterManager.Instance.GetClosestHealableAlly(character.value));
    }

    protected override void OnUpdate()
    {
        animatorStateInfo = character.value.animatorReference.GetCurrentAnimatorStateInfo(0);
        //wait for animation to finish
        if (animatorStateInfo.IsName("Attack") &&animatorStateInfo.normalizedTime >= 1f)
        {
            EndAction(true);
        }
            
    }

    protected override void OnStop()
    {
        character.value.SetTriggerAnimation(Utils.idlingParam);
    }
}

