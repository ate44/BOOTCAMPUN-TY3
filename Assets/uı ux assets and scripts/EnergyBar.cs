using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnergyBar : MonoBehaviour
{
    public Image energyBarImage;
    public Sprite energy100;
    public Sprite energy70;
    public Sprite energy40;
    public Sprite energy10;
    public Button decreaseEnergyButton;

    private int energy = 100;
    private bool isButtonPressed = false;
    private Coroutine energyRegenCoroutine;

    void Start()
    {
        UpdateEnergyBar();
        decreaseEnergyButton.onClick.AddListener(DecreaseEnergy);
        decreaseEnergyButton.onClick.AddListener(OnPointerDown);
        decreaseEnergyButton.onClick.AddListener(OnPointerUp);
    }

    void DecreaseEnergy()
    {
        energy -= 30;
        if (energy < 0)
        {
            energy = 0;
        }
        UpdateEnergyBar();
    }

    void UpdateEnergyBar()
    {
        if (energy >= 100)
        {
            energyBarImage.sprite = energy100;
        }
        else if (energy >= 70)
        {
            energyBarImage.sprite = energy70;
        }
        else if (energy >= 40)
        {
            energyBarImage.sprite = energy40;
        }
        else if (energy >= 10)
        {
            energyBarImage.sprite = energy10;
        }
    }

    public void OnPointerDown()
    {
        isButtonPressed = true;
        if (energyRegenCoroutine != null)
        {
            StopCoroutine(energyRegenCoroutine);
        }
    }

    public void OnPointerUp()
    {
        isButtonPressed = false;
        energyRegenCoroutine = StartCoroutine(RegenerateEnergy());
    }

    private IEnumerator RegenerateEnergy()
    {
        while (!isButtonPressed && energy < 100)
        {
            yield return new WaitForSeconds(1f);

            energy += 30;
            if (energy > 100)
            {
                energy = 100;
            }
            UpdateEnergyBar();
        }
    }
}
