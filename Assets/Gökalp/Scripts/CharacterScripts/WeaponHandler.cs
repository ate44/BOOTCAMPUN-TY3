using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// WeaponHandler sýnýfý, oyuncunun silahýnýn ekipman ve dinlenme pozisyonlarýný yönetir
public class WeaponHandler : MonoBehaviour
{
    // Silah eylemlerini belirten enum
    public enum Action { Equip, Unequip };

    // Silahýn referansý
    public Transform weapon;
    // Silahýn elde tutulduðu yerin referansý
    public Transform weaponHandle;
    // Silahýn býrakýldýðý pozisyonunun referansý
    public Transform weaponRestPose;

    // Silahýn pozisyonunu sýfýrlayan metod
    public void ResetWeapon(Action action)
    {
        // Eylem Equip ise silahý tutma yerine ayarla
        if (action == Action.Equip)
        {
            weapon.SetParent(weaponHandle);
        }
        // Eylem Unequip ise silahý dinlenme pozisyonuna ayarla
        else
        {
            weapon.SetParent(weaponRestPose);
        }

        // Silahýn yerel dönüþümünü ve pozisyonunu sýfýrla
        weapon.localRotation = Quaternion.identity;
        weapon.localPosition = Vector3.zero;
    }
}
