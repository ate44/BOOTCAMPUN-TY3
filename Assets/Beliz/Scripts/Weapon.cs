using UnityEngine;

public class Weapon : MonoBehaviour
{

    private Stamina staminaController;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            FireElementalController enemy = other.GetComponent<FireElementalController>();
            GoblinController enemy2 = other.GetComponent<GoblinController>();
            TrollController enemy3 = other.GetComponent<TrollController>();
            EarthElementalController enemy4 = other.GetComponent<EarthElementalController>();
            VikingController enemy5 = other.GetComponent<VikingController>();
            WarriorController enemy6 = other.GetComponent<WarriorController>();
            
            if (enemy != null)
            {
                enemy.TakeDamage();
            }

            if (enemy2 != null)
            {
                enemy2.TakeDamage();
            }
            
            if (enemy3 != null)
            {
                enemy3.TakeDamage();
            }
            
            if (enemy4 != null)
            {
                enemy4.TakeDamage();
            }
            
            if (enemy5 != null)
            {
                enemy5.TakeDamage();
            }
            
            if (enemy6 != null)
            {
                enemy6.TakeDamage();
            }
        }
    }
}