using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public float stamina;
    public float normalWalkDecrease;
    public float specialWalkDecrease;
    public float attackDecrease;
    public Slider StaminaBar;
    private float maxStamina;
    public bool isMoving;
    public bool isRunning;
    public bool isAttacking;

    void Start()
    {
        maxStamina = stamina;
        StaminaBar.maxValue = maxStamina;
        StaminaBar.value = stamina;
    }

    void Update()
    {
        if (isAttacking)
        {
            EnerjiAzalt(attackDecrease);
        }
        else if (isRunning)
        {
            EnerjiAzalt(specialWalkDecrease);
        }
        else if (isMoving)
        {
            EnerjiAzalt(normalWalkDecrease);
        }
        else
        {
            EnerjiCogalt();
        }

        StaminaBar.value = stamina;
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
    }

    private void EnerjiAzalt(float miktar)
    {
        if (stamina > 0)
            stamina -= miktar * Time.deltaTime;
    }

    private void EnerjiCogalt()
    {
        if (stamina < maxStamina)
            stamina += normalWalkDecrease * Time.deltaTime; // Enerji geri kazaným hýzý normal yürüyüþ hýzýna eþit
    }
}
