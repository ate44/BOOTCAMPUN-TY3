using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    public int damage = 10; // Düþmanýn silahýnýn vereceði hasar miktarý

    private void OnTriggerEnter(Collider other)
    {
        // Eðer çarpýþan nesne "Player" tagine sahipse
        if (other.CompareTag("Player"))
        {
            Debug.Log("1");
            // Player nesnesinin CanBari scriptine eriþim saðla
            CanBari playerHealth = other.GetComponentInParent<CanBari>();
            if (playerHealth != null)
            {
                Debug.Log("2");
                // Hasar ver ve HasarAlindi metodunu çaðýr
                playerHealth.can -= damage;
                playerHealth.HasarAlindi();
                Debug.Log("Player hit! Health: " + playerHealth.can);
            }
        }
    }
}
