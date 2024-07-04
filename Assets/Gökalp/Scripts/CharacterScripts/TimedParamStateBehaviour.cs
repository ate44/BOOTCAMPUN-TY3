using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bu s�n�f, belirli bir s�re boyunca bir parametreyi ayarlamak i�in kullan�lan bir StateMachineBehaviour t�revidir.
public class TimedParamStateBehaviour : StateMachineBehaviour
{
    // Ayarlanacak parametrenin ad�
    public string paramName;

    // Parametrenin varsay�lan de�eri
    public bool setDefaultValue;

    // Parametrenin aktif olaca�� ba�lang�� ve biti� zaman�
    public float start, end;

    // ��k�� bekleniyor mu?
    private bool waitForExit;

    // Ge�i� ��k��� tetiklendi mi?
    private bool onTransitionExitTriggered;

    // Bu metod, bir ge�i� ba�lad���nda ve durum makinesi bu durumu de�erlendirmeye ba�lad���nda �a�r�l�r
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        waitForExit = false;
        onTransitionExitTriggered = false;
        // Parametrenin ba�lang�� de�erini ayarla
        animator.SetBool(paramName, !setDefaultValue);
    }

    // Bu metod, OnStateEnter ve OnStateExit geri �a�r�lar� aras�ndaki her g�ncelleme �er�evesinde �a�r�l�r
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Ge�i� ��k���n� kontrol et
        if (CheckOnTransitionExit(animator, layerIndex))
        {
            OnStateTransitionExit(animator);
        }

        // Parametreyi belirli bir s�re boyunca ayarla
        if (!onTransitionExitTriggered && stateInfo.normalizedTime >= start && stateInfo.normalizedTime <= end)
        {
            animator.SetBool(paramName, setDefaultValue);
        }
    }

    // Ge�i� ��k��� oldu�unda �a�r�l�r
    private void OnStateTransitionExit(Animator animator)
    {
        animator.SetBool(paramName, !setDefaultValue);
    }

    // Bu metod, bir ge�i� bitti�inde ve durum makinesi bu durumu de�erlendirmeyi bitirdi�inde �a�r�l�r
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!onTransitionExitTriggered)
        {
            animator.SetBool(paramName, !setDefaultValue);
        }
    }

    // Ge�i� ��k���n� kontrol eden metod
    private bool CheckOnTransitionExit(Animator animator, int layerIndex)
    {
        // ��k�� bekleniyor mu kontrol et
        if (!waitForExit && animator.GetNextAnimatorStateInfo(layerIndex).fullPathHash == 0)
        {
            waitForExit = true;
        }

        // Ge�i� ��k��� tetiklendiyse ve ge�i�teyse true d�nd�r
        if (!onTransitionExitTriggered && waitForExit && animator.IsInTransition(layerIndex))
        {
            onTransitionExitTriggered = true;
            return true;
        }

        return false;
    }
}
