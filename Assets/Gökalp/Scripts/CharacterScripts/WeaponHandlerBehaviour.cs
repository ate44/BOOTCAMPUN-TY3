using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// WeaponHandlerBehaviour s�n�f�, Animator StateMachine i�inde belirli bir zamanda bir silah eylemini tetiklemek i�in kullan�l�r
public class WeaponHandlerBehaviour : StateMachineBehaviour
{
    // Silah eylemini belirten parametre
    public WeaponHandler.Action action;
    // Eylemin tetiklenece�i zaman
    public float eventTime;
    // Eylemin tetiklenip tetiklenmedi�ini belirten bayrak
    private bool eventTriggered;

    // OnStateEnter, bir ge�i� ba�lad���nda ve durum makinesi bu durumu de�erlendirmeye ba�lad���nda �a�r�l�r
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Eylem hen�z tetiklenmedi
        eventTriggered = false;
    }

    // OnStateUpdate, OnStateEnter ve OnStateExit geri �a�r�lar� aras�ndaki her g�ncelleme �er�evesinde �a�r�l�r
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Normalle�tirilmi� zaman eventTime'a ula�t�ysa ve eylem hen�z tetiklenmediyse
        if (stateInfo.normalizedTime >= eventTime && !eventTriggered)
        {
            // Eylemi tetikledik
            eventTriggered = true;
            // WeaponHandler bile�enini bul ve ResetWeapon metodunu �a��r
            animator.GetComponentInParent<WeaponHandler>().ResetWeapon(action);
        }
    }
}
