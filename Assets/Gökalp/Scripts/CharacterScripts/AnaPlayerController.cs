using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// AnaPlayerController s�n�f�, oyuncunun hareketlerini ve animasyonlar�n� kontrol eder
public class AnaPlayerController : MonoBehaviour
{
    // Oyuncu i�in referans
    public Transform player;

    // Ana kamera referans�
    private Camera mainCamera;

    // Oyuncunun animasyonuna referans
    private Animator anim;

    // Hareket y�n�n� kontrol eden vekt�r
    private Vector3 direction;

    // Silah�n ku�an�l�p ku�an�lmad���n� belirten bayrak
    private bool isWeaponEquipped = false;

    // Hedef kilitlenme i�in referans (d��man)
    public Transform targetLock;

    // Hedef kilitlenip kilitlenmedi�ini belirten bayrak
    public bool isTargetLocked = false;

    // D�nme h�z� i�in aral�k belirleyen parametre
    [Range(20f, 80f)]
    public float rotationSpeed = 20f;
    public float moveSpeed = 1f; //animasyonlar aras�ndaki ge�i�i smooth yapmak i�in bir bekleme s�resi

    // Ba�lang��ta �al��t�r�lan metod
    private void Start()
    {
        // Fare imlecini kilitle
        Cursor.lockState = CursorLockMode.Locked;
        // Ana kameray� al
        mainCamera = Camera.main;
        // Animator bile�enini al
        anim = GetComponentInChildren<Animator>();
    }

    // Her �er�evede �al��t�r�lan metod
    private void Update()
    {
        // Y�n� girdi eksenlerinden hesapla
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // Girdi verilerini i�le
        HandleInputData();

        // Hedef kilitlenmi�se hedefe g�re, de�ilse kameraya g�re d�n
        if (isTargetLocked)
            HandleTargetLockedRotation();
        else
            HandleRotation();
    }

    // Oyuncunun d�nmesini kontrol eden metod
    private void HandleRotation()
    {
        // Kameran�n d�n�� y�n�n� hesapla
        Vector3 rotationOffset = mainCamera.transform.TransformDirection(direction);
        rotationOffset.y = 0;
        // Oyuncunun ileri y�n�n� yava��a kameran�n y�n�ne do�ru de�i�tir
        player.forward += Vector3.Lerp(player.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }

    // Hedefe kilitli durumda d�nmeyi kontrol eden metod
    private void HandleTargetLockedRotation()
    {
        // Hedefin d�n�� y�n�n� hesapla
        Vector3 rotationOffset = targetLock.transform.position - player.position;
        rotationOffset.y = 0;
        // Oyuncunun ileri y�n�n� yava��a hedefin y�n�ne do�ru de�i�tir
        player.forward += Vector3.Lerp(player.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }

    // Girdi verilerini i�leyen metod
    private void HandleInputData()
    {
        // Animat�rdeki parametreleri girdi verilerine g�re ayarla
        anim.SetFloat("Speed", Vector3.ClampMagnitude(direction, 1).magnitude, moveSpeed, Time.deltaTime);
        anim.SetFloat("Horizontal", direction.x, moveSpeed, Time.deltaTime);
        anim.SetFloat("Vertical", direction.z, moveSpeed, Time.deltaTime);

        // Animat�rden silah�n ku�an�l�p ku�an�lmad���n� kontrol et
        isWeaponEquipped = anim.GetBool("IsWeaponEquipped");
        // Animat�rden hedefin kilitlenip kilitlenmedi�ini kontrol et
        isTargetLocked = anim.GetBool("IsTargetLocked");

        // Silah ku�an�lm��sa ve bo�luk tu�una bas�lm��sa hedef kilidini de�i�tir
        if (isWeaponEquipped && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("IsTargetLocked", !isTargetLocked);
            isTargetLocked = !isTargetLocked;
        }
        // F tu�una bas�lm��sa ve oyuncu sald�rm�yorsa silah ku�anma durumunu de�i�tir
        else if (Input.GetKeyDown(KeyCode.F) && !anim.GetBool("IsAttacking"))
        {
            anim.SetBool("IsWeaponEquipped", !isWeaponEquipped);
            isWeaponEquipped = !isWeaponEquipped;

            // Silah ku�anmam��sa hedef kilidini kapat
            if (isWeaponEquipped == false)
            {
                anim.SetBool("IsTargetLocked", false);
                isTargetLocked = false;
            }
        }
    }
}
