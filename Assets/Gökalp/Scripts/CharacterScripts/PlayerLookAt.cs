using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerLookAt s�n�f�, oyuncunun kameran�n bakt��� yere bakmas�n� sa�lar
public class PlayerLookAt : MonoBehaviour
{
    // Animator bile�enine referans
    Animator anim;
    // Ana kamera referans�
    Camera mainCamera;

    // Start, ilk karede �al��t�r�l�r
    void Start()
    {
        // Animator bile�enini al
        anim = GetComponent<Animator>();
        // Ana kameray� al
        mainCamera = Camera.main;
    }

    // OnAnimatorIK, Animator'�n Inverse Kinematics (IK) hesaplamalar� i�in �a�r�l�r
    private void OnAnimatorIK(int layerIndex)
    {
        // Bak�� a��rl���n� ayarla
        anim.SetLookAtWeight(1f, .5f, 1f, .5f);

        // Kameran�n ileri y�n�nde bir ���n olu�tur
        Ray lookAtRay = new Ray(transform.position, mainCamera.transform.forward);

        // Oyuncunun bakaca�� pozisyonu ���n�n 25 birim ilerisindeki nokta olarak ayarla
        anim.SetLookAtPosition(lookAtRay.GetPoint(25));
    }
}
