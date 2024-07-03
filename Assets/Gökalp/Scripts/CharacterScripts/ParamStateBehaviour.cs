using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamStateBehaviour : StateMachineBehaviour
{
    public SetParamSateData[] paramStateData;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (SetParamSateData data in paramStateData)
        {
            animator.SetBool(data.paramName, data.setDefaultState);
        }
    }

    [Serializable]
    public struct SetParamSateData
    {
        public string paramName;
        public bool setDefaultState;
    }

}
