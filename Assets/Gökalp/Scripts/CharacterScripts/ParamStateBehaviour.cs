using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ParamStateBehaviour sýnýfý, Animator StateMachine'deki bir durum için belirli parametreleri ayarlamak için kullanýlýr
public class ParamStateBehaviour : StateMachineBehaviour
{
    // Parametre adý ve durumunu içeren veri yapýsý
    public SetParamSateData[] paramStateData;

    // OnStateEnter, bir geçiþ baþladýðýnda ve durum makinesi bu durumu deðerlendirmeye baþladýðýnda çaðrýlýr
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // paramStateData dizisindeki her bir veri için parametreleri ayarla
        foreach (SetParamSateData data in paramStateData)
        {
            // Animator üzerindeki boolean parametreyi ayarla
            animator.SetBool(data.paramName, data.setDefaultState);
        }
    }

    // Parametre adý ve durumunu tutan yapý
    [Serializable] //parametre adýný ve durumunu unity editörden girebilmemiz için
    public struct SetParamSateData
    {
        // Parametre adý
        public string paramName;
        // Varsayýlan durumu
        public bool setDefaultState;
    }
}
