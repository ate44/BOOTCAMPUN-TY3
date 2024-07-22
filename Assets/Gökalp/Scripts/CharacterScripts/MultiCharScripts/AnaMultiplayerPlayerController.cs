using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class AnaMultiplayerPlayerController : NetworkBehaviour
{
    public Transform player;
    private Camera mainCamera;
    private Animator anim;
    private Vector3 direction;
    private bool isWeaponEquipped = false;
    public Transform targetLock;
    public bool isTargetLocked = false;
    [Range(20f, 80f)] public float rotationSpeed = 20f;
    public float moveSpeed = 1f;
    private bool canRotate = true;
    public ParticleSystem dustEffect;
    [SerializeField] private Transform canTransform;

    private NetworkVariable<Vector3> networkedPosition = new NetworkVariable<Vector3>();
    private NetworkVariable<Quaternion> networkedRotation = new NetworkVariable<Quaternion>();

    [SerializeField] private NetworkMovementComponent networkMovementComponent;

    public override void OnNetworkSpawn()
    {
        CinemachineStateDrivenCamera cinemachineStateDrivenCamera = canTransform.gameObject.GetComponent<CinemachineStateDrivenCamera>();

        if (IsOwner)
        {
            cinemachineStateDrivenCamera.Priority = 1;
        }
        else
        {
            cinemachineStateDrivenCamera.Priority = 0;
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
        if (!IsOwner) return;

        // Calculate direction from input axes
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // Process input data
        HandleInputData();

        float lookX = Input.GetAxis("Mouse X");
        float lookY = Input.GetAxis("Mouse Y");

        // Rotate towards the target or camera based on lock state
        if (isTargetLocked)
            HandleTargetLockedRotation();
        else
            HandleRotation();

        // Process local player movement and send updates to the server
        if (IsClient && IsLocalPlayer)
        {
            networkMovementComponent.ProcessLocalPlayerMovement(direction, lookX, lookY);
            networkedPosition.Value = transform.position;
            networkedRotation.Value = transform.rotation;
        }
    }

    private void HandleRotation()
    {
        if (!canRotate) return;
        Vector3 rotationOffset = mainCamera.transform.TransformDirection(direction);
        rotationOffset.y = 0;
        player.forward = Vector3.Lerp(player.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }

    private void HandleTargetLockedRotation()
    {
        Vector3 rotationOffset = targetLock.transform.position - player.position;
        rotationOffset.y = 0;
        float lookDirection = Vector3.SignedAngle(player.forward, rotationOffset, Vector3.up);
        anim.SetFloat("LookDirection", lookDirection);

        if (anim.GetFloat("Speed") > .1f)
        {
            player.forward = Vector3.Lerp(player.forward, rotationOffset, Time.deltaTime * rotationSpeed);
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
            anim.SetBool("IsTargetLocked", !isTargetLocked);
            isTargetLocked = !isTargetLocked;
        }
        else if (Input.GetKeyDown(KeyCode.F) && !anim.GetBool("IsAttacking"))
        {
            anim.SetBool("IsWeaponEquipped", !isWeaponEquipped);
            isWeaponEquipped = !isWeaponEquipped;

            if (!isWeaponEquipped)
            {
                anim.SetBool("IsTargetLocked", false);
                isTargetLocked = false;
            }
        }
    }

    private void CreateDust()
    {
        dustEffect.Play();
    }
}
