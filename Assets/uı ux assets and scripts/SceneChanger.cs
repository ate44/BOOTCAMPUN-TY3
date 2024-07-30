using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class SceneChanger : MonoBehaviour
{
    public GameObject player;
    public GameObject marketPanel;
    public GameObject UIPanel;
    public Transform outMarketPos;
    public Transform marketExitPos; // Oyuncuyu marketten çýkarýrken taþýmak istediðiniz pozisyon
    public static bool isPlayerInMarket = false;

    void Awake()
    {
        marketPanel.SetActive(false);
    }

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        if (isPlayerInMarket)
        {
            UIPanel.SetActive(false);
            marketPanel.SetActive(true);
        }
    }

    public void ExitMenu()
    {
        // Oyuncuyu marketten çýkarýn
        player.transform.position = marketExitPos.position;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        isPlayerInMarket = false;
        marketPanel.SetActive(false);
        UIPanel.SetActive(true);

        // Tüm scriptleri yeniden etkinleþtirin
        EnableScripts();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                DisableScripts();
            }

            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;

            isPlayerInMarket = true;
            player.transform.position = outMarketPos.position;
        }
    }

    void DisableScripts()
    {
        var anaPlayerController = player.GetComponent<AnaPlayerController>();
        if (anaPlayerController != null) anaPlayerController.enabled = false;

        var bloodActivator = player.GetComponent<BloodActivator>();
        if (bloodActivator != null) bloodActivator.enabled = false;

        var hittable = player.GetComponent<Hittable>();
        if (hittable != null) hittable.enabled = false;

        var hittableRigid = player.GetComponent<HittableRigid>();
        if (hittableRigid != null) hittableRigid.enabled = false;

        var hittableRigidHandler = player.GetComponent<HittableRigidHandler>();
        if (hittableRigidHandler != null) hittableRigidHandler.enabled = false;

        var meleeController = player.GetComponent<MeleeController>();
        if (meleeController != null) meleeController.enabled = false;

        var playerLookAt = player.GetComponent<PlayerLookAt>();
        if (playerLookAt != null) playerLookAt.enabled = false;

        var weaponHandler = player.GetComponent<WeaponHandler>();
        if (weaponHandler != null) weaponHandler.enabled = false;
    }

    void EnableScripts()
    {
        var anaPlayerController = player.GetComponent<AnaPlayerController>();
        if (anaPlayerController != null) anaPlayerController.enabled = true;

        var bloodActivator = player.GetComponent<BloodActivator>();
        if (bloodActivator != null) bloodActivator.enabled = true;

        var hittable = player.GetComponent<Hittable>();
        if (hittable != null) hittable.enabled = true;

        var hittableRigid = player.GetComponent<HittableRigid>();
        if (hittableRigid != null) hittableRigid.enabled = true;

        var hittableRigidHandler = player.GetComponent<HittableRigidHandler>();
        if (hittableRigidHandler != null) hittableRigidHandler.enabled = true;

        var meleeController = player.GetComponent<MeleeController>();
        if (meleeController != null) meleeController.enabled = true;

        var playerLookAt = player.GetComponent<PlayerLookAt>();
        if (playerLookAt != null) playerLookAt.enabled = true;

        var weaponHandler = player.GetComponent<WeaponHandler>();
        if (weaponHandler != null) weaponHandler.enabled = true;
    }
}
