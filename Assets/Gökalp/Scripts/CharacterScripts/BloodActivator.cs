using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodActivator : MonoBehaviour
{
    [SerializeField] private ParticleSystem bloodEffect;
    private Stamina staminaController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {

            bloodEffect.Play();


        }
    }

    
}
