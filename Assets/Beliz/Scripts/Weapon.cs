using UnityEngine;

public class Weapon : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            FireElementalController enemy = other.GetComponent<FireElementalController>();
            if (enemy != null)
            {
                enemy.TakeDamage();
            }
        }
    }
}