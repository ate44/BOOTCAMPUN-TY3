using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class SceneChanger : MonoBehaviour
{
    public GameObject player; // Player GameObject'ini referans olarak tutmak için
    public GameObject marketPanel;
    public GameObject UIPanel;
    public Transform outMarketPos;
    public static bool isPlayerInMarket = false;

    void Awake()
    {
        marketPanel.SetActive(false);
    }

    void Start()
    {
        // Player GameObject'ini bul ve referans olarak sakla
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
        //player.transform.localPosition = outMarketPos.position;
        //player.transform.position = outMarketPos.position;
        isPlayerInMarket = false;
        marketPanel.SetActive(false);
        UIPanel.SetActive(true);


    }

    // Çarpýþma anýnda tetiklenecek olan metod
    void OnTriggerEnter(Collider collision)
    {
        // Çarpýþýlan objenin tag'ý "Player" ise sahneyi deðiþtir
        if (collision.CompareTag("Player"))
        {
            // Tüm scriptleri devre dýþý býrak
            if (player != null)
            {
                DisableScripts();
            }

            // Fare imlecini görünür ve serbest hale getir
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;

            isPlayerInMarket = true;
            collision.transform.position = outMarketPos.transform.position;
            //collision.attachedRigidbody.velocity = Vector3.zero;
        }
    }

    // Tüm scriptleri devre dýþý býrakan metod
    void DisableScripts()
    {
        // Her script için ayný kontrol ve devre dýþý býrakma iþlemi
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
}

