using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// WeaponHandler s�n�f�, oyuncunun silah�n�n ekipman ve dinlenme pozisyonlar�n� y�netir
public class WeaponHandler : MonoBehaviour
{
    // Silah eylemlerini belirten enum
    public enum Action { Equip, Unequip };

    // Silah�n referans�
    public Transform weapon;
    // Silah�n elde tutuldu�u yerin referans�
    public Transform weaponHandle;
    // Silah�n b�rak�ld��� pozisyonunun referans�
    public Transform weaponRestPose;

    // Silah�n pozisyonunu s�f�rlayan metod
    public void ResetWeapon(Action action)
    {
        // Eylem Equip ise silah� tutma yerine ayarla
        if (action == Action.Equip)
        {
            weapon.SetParent(weaponHandle);
        }
        // Eylem Unequip ise silah� dinlenme pozisyonuna ayarla
        else
        {
            weapon.SetParent(weaponRestPose);
        }

        // Silah�n yerel d�n���m�n� ve pozisyonunu s�f�rla
        weapon.localRotation = Quaternion.identity;
        weapon.localPosition = Vector3.zero;
    }
}
