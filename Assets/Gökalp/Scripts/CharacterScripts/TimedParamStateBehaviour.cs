using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bu sýnýf, belirli bir süre boyunca bir parametreyi ayarlamak için kullanýlan bir StateMachineBehaviour türevidir.
public class TimedParamStateBehaviour : StateMachineBehaviour
{
    // Ayarlanacak parametrenin adý
    public string paramName;

    // Parametrenin varsayýlan deðeri
    public bool setDefaultValue;

    // Parametrenin aktif olacaðý baþlangýç ve bitiþ zamaný
    public float start, end;

    // Çýkýþ bekleniyor mu?
    private bool waitForExit;

    // Geçiþ çýkýþý tetiklendi mi?
    private bool onTransitionExitTriggered;

    // Bu metod, bir geçiþ baþladýðýnda ve durum makinesi bu durumu deðerlendirmeye baþladýðýnda çaðrýlýr
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        waitForExit = false;
        onTransitionExitTriggered = false;
        // Parametrenin baþlangýç deðerini ayarla
        animator.SetBool(paramName, !setDefaultValue);
    }

    // Bu metod, OnStateEnter ve OnStateExit geri çaðrýlarý arasýndaki her güncelleme çerçevesinde çaðrýlýr
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Geçiþ çýkýþýný kontrol et
        if (CheckOnTransitionExit(animator, layerIndex))
        {
            OnStateTransitionExit(animator);
        }

        // Parametreyi belirli bir süre boyunca ayarla
        if (!onTransitionExitTriggered && stateInfo.normalizedTime >= start && stateInfo.normalizedTime <= end)
        {
            animator.SetBool(paramName, setDefaultValue);
        }
    }

    // Geçiþ çýkýþý olduðunda çaðrýlýr
    private void OnStateTransitionExit(Animator animator)
    {
        animator.SetBool(paramName, !setDefaultValue);
    }

    // Bu metod, bir geçiþ bittiðinde ve durum makinesi bu durumu deðerlendirmeyi bitirdiðinde çaðrýlýr
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!onTransitionExitTriggered)
        {
            animator.SetBool(paramName, !setDefaultValue);
        }
    }

    // Geçiþ çýkýþýný kontrol eden metod
    private bool CheckOnTransitionExit(Animator animator, int layerIndex)
    {
        // Çýkýþ bekleniyor mu kontrol et
        if (!waitForExit && animator.GetNextAnimatorStateInfo(layerIndex).fullPathHash == 0)
        {
            waitForExit = true;
        }

        // Geçiþ çýkýþý tetiklendiyse ve geçiþteyse true döndür
        if (!onTransitionExitTriggered && waitForExit && animator.IsInTransition(layerIndex))
        {
            onTransitionExitTriggered = true;
            return true;
        }

        return false;
    }
}
