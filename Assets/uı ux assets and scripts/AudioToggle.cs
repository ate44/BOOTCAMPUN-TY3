using UnityEngine;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    public Sprite soundOnSprite;  // Sesi açma görseli
    public Sprite soundOffSprite; // Sesi kapatma görseli
    private Button button; // Buton referansý
    private Image buttonImage; // Butonun Image bileþeni

    private void Start()
    {
        button = GetComponent<Button>();
        buttonImage = button.GetComponent<Image>();

        // Ýlk durumu ayarla
        UpdateButtonImage();

        // Buton týklama olayýný dinle
        button.onClick.AddListener(ToggleSound);
    }

    private void ToggleSound()
    {
        AudioSource audioSource = AudioManager.Instance.audioSource;

        if (audioSource.isPlaying)
        {
            audioSource.Pause(); // Müziði duraklat
        }
        else
        {
            audioSource.Play(); // Müziði çal
        }

        UpdateButtonImage(); // Görseli güncelle
    }

    private void UpdateButtonImage()
    {
        AudioSource audioSource = AudioManager.Instance.audioSource;

        if (audioSource.isPlaying)
        {
            buttonImage.sprite = soundOnSprite; // Sesi açma görseli
        }
        else
        {
            buttonImage.sprite = soundOffSprite; // Sesi kapatma görseli
        }
    }
}
