using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// WeaponHandlerBehaviour sýnýfý, Animator StateMachine içinde belirli bir zamanda bir silah eylemini tetiklemek için kullanýlýr
public class WeaponHandlerBehaviour : StateMachineBehaviour
{
    // Silah eylemini belirten parametre
    public WeaponHandler.Action action;
    // Eylemin tetikleneceði zaman
    public float eventTime;
    // Eylemin tetiklenip tetiklenmediðini belirten bayrak
    private bool eventTriggered;

    // OnStateEnter, bir geçiþ baþladýðýnda ve durum makinesi bu durumu deðerlendirmeye baþladýðýnda çaðrýlýr
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Eylem henüz tetiklenmedi
        eventTriggered = false;
    }

    // OnStateUpdate, OnStateEnter ve OnStateExit geri çaðrýlarý arasýndaki her güncelleme çerçevesinde çaðrýlýr
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Normalleþtirilmiþ zaman eventTime'a ulaþtýysa ve eylem henüz tetiklenmediyse
        if (stateInfo.normalizedTime >= eventTime && !eventTriggered)
        {
            // Eylemi tetikledik
            eventTriggered = true;
            // WeaponHandler bileþenini bul ve ResetWeapon metodunu çaðýr
            animator.GetComponentInParent<WeaponHandler>().ResetWeapon(action);
        }
    }
}
