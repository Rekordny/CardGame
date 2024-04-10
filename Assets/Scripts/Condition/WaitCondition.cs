using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCondition : ConditionTask
{
    public BBParameter<float> secondsToWait;
    private float waitDuration = 0f;
    protected override string OnInit()
    {
        return null;
    }

    protected override void OnEnable()
    {
        waitDuration = 0f;
    }

    protected override bool OnCheck()
    {
        waitDuration += Time.deltaTime;
        return waitDuration >= secondsToWait.value;
    }
}
