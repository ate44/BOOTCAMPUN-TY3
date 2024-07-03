using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ParamStateBehaviour s�n�f�, Animator StateMachine'deki bir durum i�in belirli parametreleri ayarlamak i�in kullan�l�r
public class ParamStateBehaviour : StateMachineBehaviour
{
    // Parametre ad� ve durumunu i�eren veri yap�s�
    public SetParamSateData[] paramStateData;

    // OnStateEnter, bir ge�i� ba�lad���nda ve durum makinesi bu durumu de�erlendirmeye ba�lad���nda �a�r�l�r
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // paramStateData dizisindeki her bir veri i�in parametreleri ayarla
        foreach (SetParamSateData data in paramStateData)
        {
            // Animator �zerindeki boolean parametreyi ayarla
            animator.SetBool(data.paramName, data.setDefaultState);
        }
    }

    // Parametre ad� ve durumunu tutan yap�
    [Serializable] //parametre ad�n� ve durumunu unity edit�rden girebilmemiz i�in
    public struct SetParamSateData
    {
        // Parametre ad�
        public string paramName;
        // Varsay�lan durumu
        public bool setDefaultState;
    }
}
