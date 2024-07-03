using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform player; //oyuncu için referans

    private Camera mainCamera; //kamera kontrolü

    private Animator anim; //oyuncunun animasyonuna referans

    private Vector3 direction; //hareketi kontrol ediyoruz

    private bool isWeaponEquipped = false;

    public Transform targetLock; //enemy

    public bool isTargetLocked = false;

    [Range(20f, 80f)]
    public float rotationSpeed = 20f;
    public float moveSpeed = 1f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        HandleInputData();
        
        if(isTargetLocked) HandleTargetLockedRotation();
        else HandleRotation();

        if (Input.GetKeyDown(KeyCode.Mouse0)) StartCoroutine(Attack());

    }

    private void HandleRotation()
    {
        Vector3 rotationOffset = mainCamera.transform.TransformDirection(direction);
        rotationOffset.y = 0;
        player.forward += Vector3.Lerp(player.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }

    private void HandleTargetLockedRotation()
    {
        Vector3 rotationOffset = targetLock.transform.position - player.position;
        rotationOffset.y = 0;
        player.forward += Vector3.Lerp(player.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }

    private void HandleInputData()
    {
        anim.SetFloat("Speed", Vector3.ClampMagnitude(direction,1).magnitude, moveSpeed, Time.deltaTime);
        anim.SetFloat("Horizontal", direction.x, moveSpeed, Time.deltaTime);
        anim.SetFloat("Vertical", direction.z, moveSpeed, Time.deltaTime);
        isWeaponEquipped = anim.GetBool("IsWeaponEquipped");
        isTargetLocked = anim.GetBool("IsTargetLocked");

        if (isWeaponEquipped && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("IsTargetLocked", !isTargetLocked);
            isTargetLocked = !isTargetLocked;
        }
        else if (Input.GetKeyDown(KeyCode.F)) 
        {          
            anim.SetBool("IsWeaponEquipped", !isWeaponEquipped);
            isWeaponEquipped = !isWeaponEquipped;

            if(isWeaponEquipped == false)
            {
                anim.SetBool("IsTargetLocked", false);
                isTargetLocked = false;
            }
        }
    }

    private IEnumerator Attack()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(1.5f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
    }
}
