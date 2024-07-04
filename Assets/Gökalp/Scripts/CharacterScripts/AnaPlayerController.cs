using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// AnaPlayerController sýnýfý, oyuncunun hareketlerini ve animasyonlarýný kontrol eder
public class AnaPlayerController : MonoBehaviour
{
    // Oyuncu için referans
    public Transform player;

    // Ana kamera referansý
    private Camera mainCamera;

    // Oyuncunun animasyonuna referans
    private Animator anim;

    // Hareket yönünü kontrol eden vektör
    private Vector3 direction;

    // Silahýn kuþanýlýp kuþanýlmadýðýný belirten bayrak
    private bool isWeaponEquipped = false;

    // Hedef kilitlenme için referans (düþman)
    public Transform targetLock;

    // Hedef kilitlenip kilitlenmediðini belirten bayrak
    public bool isTargetLocked = false;

    // Dönme hýzý için aralýk belirleyen parametre
    [Range(20f, 80f)]
    public float rotationSpeed = 20f;
    public float moveSpeed = 1f; //animasyonlar arasýndaki geçiþi smooth yapmak için bir bekleme süresi

    // Baþlangýçta çalýþtýrýlan metod
    private void Start()
    {
        // Fare imlecini kilitle
        Cursor.lockState = CursorLockMode.Locked;
        // Ana kamerayý al
        mainCamera = Camera.main;
        // Animator bileþenini al
        anim = GetComponentInChildren<Animator>();
    }

    // Her çerçevede çalýþtýrýlan metod
    private void Update()
    {
        // Yönü girdi eksenlerinden hesapla
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // Girdi verilerini iþle
        HandleInputData();

        // Hedef kilitlenmiþse hedefe göre, deðilse kameraya göre dön
        if (isTargetLocked)
            HandleTargetLockedRotation();
        else
            HandleRotation();
    }

    // Oyuncunun dönmesini kontrol eden metod
    private void HandleRotation()
    {
        // Kameranýn dönüþ yönünü hesapla
        Vector3 rotationOffset = mainCamera.transform.TransformDirection(direction);
        rotationOffset.y = 0;
        // Oyuncunun ileri yönünü yavaþça kameranýn yönüne doðru deðiþtir
        player.forward += Vector3.Lerp(player.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }

    // Hedefe kilitli durumda dönmeyi kontrol eden metod
    private void HandleTargetLockedRotation()
    {
        // Hedefin dönüþ yönünü hesapla
        Vector3 rotationOffset = targetLock.transform.position - player.position;
        rotationOffset.y = 0;
        // Oyuncunun ileri yönünü yavaþça hedefin yönüne doðru deðiþtir
        player.forward += Vector3.Lerp(player.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }

    // Girdi verilerini iþleyen metod
    private void HandleInputData()
    {
        // Animatördeki parametreleri girdi verilerine göre ayarla
        anim.SetFloat("Speed", Vector3.ClampMagnitude(direction, 1).magnitude, moveSpeed, Time.deltaTime);
        anim.SetFloat("Horizontal", direction.x, moveSpeed, Time.deltaTime);
        anim.SetFloat("Vertical", direction.z, moveSpeed, Time.deltaTime);

        // Animatörden silahýn kuþanýlýp kuþanýlmadýðýný kontrol et
        isWeaponEquipped = anim.GetBool("IsWeaponEquipped");
        // Animatörden hedefin kilitlenip kilitlenmediðini kontrol et
        isTargetLocked = anim.GetBool("IsTargetLocked");

        // Silah kuþanýlmýþsa ve boþluk tuþuna basýlmýþsa hedef kilidini deðiþtir
        if (isWeaponEquipped && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("IsTargetLocked", !isTargetLocked);
            isTargetLocked = !isTargetLocked;
        }
        // F tuþuna basýlmýþsa ve oyuncu saldýrmýyorsa silah kuþanma durumunu deðiþtir
        else if (Input.GetKeyDown(KeyCode.F) && !anim.GetBool("IsAttacking"))
        {
            anim.SetBool("IsWeaponEquipped", !isWeaponEquipped);
            isWeaponEquipped = !isWeaponEquipped;

            // Silah kuþanmamýþsa hedef kilidini kapat
            if (isWeaponEquipped == false)
            {
                anim.SetBool("IsTargetLocked", false);
                isTargetLocked = false;
            }
        }
    }
}
