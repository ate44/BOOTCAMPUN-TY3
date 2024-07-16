using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public float stamina;
    public float mValue;
    public Slider StaminaBar;
    float maxStamina;
    void Start()
    {
        maxStamina = stamina;
        StaminaBar.value = maxStamina;
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            EnerjiAzalt();
        else if (stamina != maxStamina)
            EnerjiCogalt();

        StaminaBar.value = stamina;
        if (stamina >= 30f)
            stamina = 30f;

        if (stamina <= 0f)
            stamina = 0f;
            

    }


    private void EnerjiAzalt()
    {
        if (stamina != 0)
            stamina -= mValue * Time.deltaTime;
    }
    private void EnerjiCogalt()
    {
        
            stamina += mValue * Time.deltaTime;
    }
}
