using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class db : StateMachineBehaviour
{
    public bool Exited;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Exited = false;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Exited = true;
    }

}
