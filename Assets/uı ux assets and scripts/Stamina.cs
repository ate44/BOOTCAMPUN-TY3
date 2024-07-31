using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public float stamina;
    public float normalWalkDecrease;
    public float specialWalkDecrease;
    public float attackDecrease;
    public float iksir;
    public Slider StaminaBar;
    private float maxStamina;
    public bool isMoving;
    public bool isRunning;
    public bool isAttacking;

    private ParaSistemi ps;

    void Start()
    {
        ps = GetComponent<ParaSistemi>();   

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
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(ps.enerjisayisi > 0)
            {
                Enerji›ksiri(iksir);
            }
            
        }
        else
        {
            EnerjiCogalt();
        }

        

        StaminaBar.value = stamina;
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
    }

    private void Enerji›ksiri(float miktar)
    {
        if (stamina < maxStamina)
        {
            ps.KullanEnergy();
            stamina += miktar * Time.deltaTime * 3;
        }
            

    }

    private void EnerjiAzalt(float miktar)
    {
        if (stamina > 0)
            stamina -= miktar * Time.deltaTime;
    }

    private void EnerjiCogalt()
    {
        if (stamina < maxStamina)
            stamina += normalWalkDecrease * Time.deltaTime; // Enerji geri kazan˝m h˝z˝ normal y¸r¸y¸˛ h˝z˝na e˛it
    }
}
