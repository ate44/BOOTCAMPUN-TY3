using UnityEngine;

public class AnaPlayerController : MonoBehaviour
{
    public Transform player;
    private Camera mainCamera;
    private Animator anim;
    private Vector3 direction;
    private bool isWeaponEquipped = false;
    public Transform[] targetLock;
    public bool isTargetLocked = false;

    [Range(20f, 80f)]
    public float rotationSpeed = 20f;
    public float moveSpeed = 1f;

    private bool canRotate = true;
    public ParticleSystem dustEffect;

    private Transform closestTarget;
    private Stamina staminaComponent;
    private ParaSistemi ps;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
        anim = GetComponentInChildren<Animator>();
        staminaComponent = GetComponent<Stamina>();
        ps = GetComponent<ParaSistemi>();
    }

    private void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        HandleInputData();

        if (isTargetLocked)
        {
            FindClosestTarget();
            HandleTargetLockedRotation();
        }
        else
        {
            HandleRotation();
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ps.KullanExtraDamage();
        }
        
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ps.KullanHealth();
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
        if (closestTarget == null) return;

        Vector3 rotationOffset = closestTarget.position - player.position;
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
            anim.SetBool("IsTargetLocked", !isTargetLocked);
            isTargetLocked = !isTargetLocked;
        }
        else if (Input.GetKeyDown(KeyCode.F) && !anim.GetBool("IsAttacking"))
        {
            anim.SetBool("IsWeaponEquipped", !isWeaponEquipped);
            isWeaponEquipped = !isWeaponEquipped;

            if (isWeaponEquipped == false)
            {
                anim.SetBool("IsTargetLocked", false);
                isTargetLocked = false;
                closestTarget = null;
            }
        }

        staminaComponent.isMoving = direction.magnitude > 0;
        staminaComponent.isRunning = isWeaponEquipped && direction.magnitude > 0;
        
    }

    private void CreateDust()
    {
        dustEffect.Play();
    }

    private void FindClosestTarget()
    {
        float closestDistance = Mathf.Infinity;
        closestTarget = null;

        foreach (var target in targetLock)
        {
            float distance = Vector3.Distance(player.position, target.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }
    }
}
