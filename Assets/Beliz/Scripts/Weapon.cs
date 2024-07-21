using UnityEngine;

public class Weapon : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            FireElementalController enemy = other.GetComponent<FireElementalController>();
            if (enemy != null)
            {
                enemy.TakeDamage();
            }
        }
    }
}