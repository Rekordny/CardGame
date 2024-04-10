using NodeCanvas.Framework;
using UnityEngine;

public class IdleAction : ActionTask
{
    public BBParameter<Animator> animator;
    private readonly int walkingParam = Animator.StringToHash("Walking");
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
        base.OnUpdate();
    }

    protected override void OnStop()
    {
        animator.value.SetBool(walkingParam, true);
    }
}
