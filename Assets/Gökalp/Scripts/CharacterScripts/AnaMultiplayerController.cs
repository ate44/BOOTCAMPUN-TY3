using System;
using Unity.Netcode;
using UnityEngine;
using Cinemachine;

public class AnaMultiplayerController : NetworkBehaviour
{
    public Transform player;

    private Camera mainCamera;
    private Animator anim;
    private Vector3 direction;
    private bool isWeaponEquipped = false;
    public Transform targetLock;
    public bool isTargetLocked = false;

    [Range(20f, 80f)]
    public float rotationSpeed = 20f;
    public float moveSpeed = 1f;

    private bool canRotate = true;
    public ParticleSystem dustEffect;

    [SerializeField] private Transform _canTransform;

    private NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>();
    private NetworkVariable<Vector3> networkRotation = new NetworkVariable<Vector3>();

    public override void OnNetworkSpawn()
    {
        CinemachineStateDrivenCamera csdc = _canTransform.gameObject.GetComponent<CinemachineStateDrivenCamera>();

        if (IsOwner)
        {
            csdc.Priority = 1;
        }
        else
        {
            csdc.Priority = 0;
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (IsOwner)
        {
            direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            HandleInputData();

            if (isTargetLocked)
                HandleTargetLockedRotation();
            else
                HandleRotation();

            // Pozisyon ve rotasyonu güncelle
            networkPosition.Value = transform.position;
            networkRotation.Value = transform.eulerAngles;
        }
        else
        {
            // Pozisyon ve rotasyonu senkronize et
            transform.position = networkPosition.Value;
            transform.eulerAngles = networkRotation.Value;
        }
    }

    private void HandleRotation()
    {
        if (!canRotate) return;

        Vector3 rotationOffset = mainCamera.transform.TransformDirection(direction);
        rotationOffset.y = 0;
        player.forward += Vector3.Lerp(player.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }

    private void HandleTargetLockedRotation()
    {
        Vector3 rotationOffset = targetLock.transform.position - player.position;
        rotationOffset.y = 0;

        float lookDirection = Vector3.SignedAngle(player.forward, rotationOffset, Vector3.up);
        anim.SetFloat("LookDirection", lookDirection);

        if (anim.GetFloat("Speed") > .1f)
        {
            player.forward += Vector3.Lerp(player.forward, rotationOffset, Time.deltaTime * rotationSpeed);
        }
    }

    private void HandleInputData()
    {
        anim.SetFloat("Speed", Vector3.ClampMagnitude(direction, 1).magnitude, moveSpeed, Time.deltaTime);
        anim.SetFloat("Horizontal", direction.x, moveSpeed, Time.deltaTime);
        anim.SetFloat("Vertical", direction.z, moveSpeed, Time.deltaTime);

        isWeaponEquipped = anim.GetBool("IsWeaponEquipped");
        isTargetLocked = anim.GetBool("IsTargetLocked");
        canRotate = anim.GetBool("CanRotate");

        float lookDirection = Vector3.SignedAngle(player.forward, Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up), Vector3.up);
        anim.SetFloat("LookDirection", lookDirection);

        if (anim.GetFloat("Speed") > 0.1f)
        {
            CreateDust();
        }
        else
        {
            dustEffect.Stop();
        }

        if (isWeaponEquipped && Input.GetKeyDown(KeyCode.Space))
        {
            SetTargetLockStateServerRpc(!isTargetLocked);
        }
        else if (Input.GetKeyDown(KeyCode.F) && !anim.GetBool("IsAttacking"))
        {
            SetWeaponEquippedStateServerRpc(!isWeaponEquipped);
        }
    }

    private void CreateDust()
    {
        dustEffect.Play();
    }

    [ServerRpc]
    private void SetTargetLockStateServerRpc(bool newState)
    {
        anim.SetBool("IsTargetLocked", newState);
        isTargetLocked = newState;
    }

    [ServerRpc]
    private void SetWeaponEquippedStateServerRpc(bool newState)
    {
        anim.SetBool("IsWeaponEquipped", newState);
        isWeaponEquipped = newState;

        if (!isWeaponEquipped)
        {
            SetTargetLockStateServerRpc(false);
        }
    }
}
