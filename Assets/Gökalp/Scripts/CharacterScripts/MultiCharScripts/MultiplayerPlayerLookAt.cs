using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

// PlayerLookAt sýnýfý, oyuncunun kameranýn baktýðý yere bakmasýný saðlar
public class MultiplayerPlayerLookAt : NetworkBehaviour
{
    // Animator bileþenine referans
    Animator anim;
    // Ana kamera referansý
    Camera mainCamera;

    // Start, ilk karede çalýþtýrýlýr
    void Start()
    {
        // Sadece yerel oyuncu için animatör bileþenini al
        if (IsLocalPlayer)
        {
            anim = GetComponent<Animator>();
            mainCamera = Camera.main;
        }
    }

    // OnAnimatorIK, Animator'ýn Inverse Kinematics (IK) hesaplamalarý için çaðrýlýr
    private void OnAnimatorIK(int layerIndex)
    {
        if (!IsLocalPlayer) return;

        // Bakýþ aðýrlýðýný ayarla
        anim.SetLookAtWeight(1f, .5f, 1f, .5f);

        // Kameranýn ileri yönünde bir ýþýn oluþtur
        Ray lookAtRay = new Ray(transform.position, mainCamera.transform.forward);

        // Oyuncunun bakacaðý pozisyonu ýþýnýn 25 birim ilerisindeki nokta olarak ayarla
        anim.SetLookAtPosition(lookAtRay.GetPoint(25));
    }
}
