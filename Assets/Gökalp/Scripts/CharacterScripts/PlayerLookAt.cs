using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerLookAt sýnýfý, oyuncunun kameranýn baktýðý yere bakmasýný saðlar
public class PlayerLookAt : MonoBehaviour
{
    // Animator bileþenine referans
    Animator anim;
    // Ana kamera referansý
    Camera mainCamera;

    // Start, ilk karede çalýþtýrýlýr
    void Start()
    {
        // Animator bileþenini al
        anim = GetComponent<Animator>();
        // Ana kamerayý al
        mainCamera = Camera.main;
    }

    // OnAnimatorIK, Animator'ýn Inverse Kinematics (IK) hesaplamalarý için çaðrýlýr
    private void OnAnimatorIK(int layerIndex)
    {
        // Bakýþ aðýrlýðýný ayarla
        anim.SetLookAtWeight(1f, .5f, 1f, .5f);

        // Kameranýn ileri yönünde bir ýþýn oluþtur
        Ray lookAtRay = new Ray(transform.position, mainCamera.transform.forward);

        // Oyuncunun bakacaðý pozisyonu ýþýnýn 25 birim ilerisindeki nokta olarak ayarla
        anim.SetLookAtPosition(lookAtRay.GetPoint(25));
    }
}
