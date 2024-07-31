using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    public int damage = 10; // Düþmanýn silahýnýn vereceði hasar miktarý

    private AudioManagerSc audioManager;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManagerSc>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene!");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // Eðer çarpýþan nesne "Player" tagine sahipse
        if (other.CompareTag("Attack"))
        {
            Debug.Log("1");
            // Player nesnesinin CanBari scriptine eriþim saðla
            CanBari playerHealth = GetComponent<CanBari>();
            if (playerHealth != null)
            {
                audioManager.PlaySFX(audioManager.wounded);
                audioManager.PlaySFX(audioManager.swordHitting);
                Debug.Log("2");
                // Hasar ver ve HasarAlindi metodunu çaðýr
                playerHealth.can -= damage;
                playerHealth.HasarAlindi();
                Debug.Log("Player hit! Health: " + playerHealth.can);
            }
            else
            {
                Debug.Log("yok");
            }
        }
    }
}
