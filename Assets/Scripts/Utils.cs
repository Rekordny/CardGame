using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static void ChangeGlobalTimeScale(float slowdownFactor)
    {
        Mathf.Clamp(slowdownFactor, 0.1f, 1f);
        Time.timeScale = slowdownFactor;
    }

    public static readonly int walkingParam = Animator.StringToHash("Walking");
    public static readonly int idlingParam = Animator.StringToHash("Idling");
    public static readonly int attackingParam = Animator.StringToHash("Attacking");
    public static readonly int dyingParam = Animator.StringToHash("Dying");

    public static readonly List<int> paramList = new List<int>() { 
        walkingParam, idlingParam, attackingParam, dyingParam
    };

}
