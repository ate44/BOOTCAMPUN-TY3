using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    public int damage = 10; // D��man�n silah�n�n verece�i hasar miktar�

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
        // E�er �arp��an nesne "Player" tagine sahipse
        if (other.CompareTag("Attack"))
        {
            Debug.Log("1");
            // Player nesnesinin CanBari scriptine eri�im sa�la
            CanBari playerHealth = GetComponent<CanBari>();
            if (playerHealth != null)
            {
                audioManager.PlaySFX(audioManager.wounded);
                audioManager.PlaySFX(audioManager.swordHitting);
                Debug.Log("2");
                // Hasar ver ve HasarAlindi metodunu �a��r
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
