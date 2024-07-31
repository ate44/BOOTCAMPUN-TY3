using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    public int damage = 10; // D��man�n silah�n�n verece�i hasar miktar�

    private void OnTriggerEnter(Collider other)
    {
        // E�er �arp��an nesne "Player" tagine sahipse
        if (other.CompareTag("Player"))
        {
            Debug.Log("1");
            // Player nesnesinin CanBari scriptine eri�im sa�la
            CanBari playerHealth = other.GetComponentInParent<CanBari>();
            if (playerHealth != null)
            {
                Debug.Log("2");
                // Hasar ver ve HasarAlindi metodunu �a��r
                playerHealth.can -= damage;
                playerHealth.HasarAlindi();
                Debug.Log("Player hit! Health: " + playerHealth.can);
            }
        }
    }
}
