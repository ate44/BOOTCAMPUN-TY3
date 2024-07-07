using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public Sprite health100;
    public Sprite health70;
    public Sprite health40;
    public Sprite health10;
    public Button decreaseHealthButton;

    private int health = 100;

    void Start()
    {
        UpdateHealthBar();
        decreaseHealthButton.onClick.AddListener(DecreaseHealth);
    }

    void DecreaseHealth()
    {
        health -= 30;
        if (health <= 0)
        {
            health = 0;
            SceneManager.LoadScene(3); // Sahne indeks 3'e geçiþ
        }
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (health == 100)
        {
            healthBarImage.sprite = health100;
        }
        else if (health == 70)
        {
            healthBarImage.sprite = health70;
        }
        else if (health == 40)
        {
            healthBarImage.sprite = health40;
        }
        else if (health == 10)
        {
            healthBarImage.sprite = health10;
        }
    }
}

