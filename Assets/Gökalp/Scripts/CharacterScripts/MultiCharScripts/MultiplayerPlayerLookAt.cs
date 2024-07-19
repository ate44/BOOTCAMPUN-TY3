using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

// PlayerLookAt s�n�f�, oyuncunun kameran�n bakt��� yere bakmas�n� sa�lar
public class MultiplayerPlayerLookAt : NetworkBehaviour
{
    // Animator bile�enine referans
    Animator anim;
    // Ana kamera referans�
    Camera mainCamera;

    // Start, ilk karede �al��t�r�l�r
    void Start()
    {
        // Sadece yerel oyuncu i�in animat�r bile�enini al
        if (IsLocalPlayer)
        {
            anim = GetComponent<Animator>();
            mainCamera = Camera.main;
        }
    }

    // OnAnimatorIK, Animator'�n Inverse Kinematics (IK) hesaplamalar� i�in �a�r�l�r
    private void OnAnimatorIK(int layerIndex)
    {
        if (!IsLocalPlayer) return;

        // Bak�� a��rl���n� ayarla
        anim.SetLookAtWeight(1f, .5f, 1f, .5f);

        // Kameran�n ileri y�n�nde bir ���n olu�tur
        Ray lookAtRay = new Ray(transform.position, mainCamera.transform.forward);

        // Oyuncunun bakaca�� pozisyonu ���n�n 25 birim ilerisindeki nokta olarak ayarla
        anim.SetLookAtPosition(lookAtRay.GetPoint(25));
    }
}
