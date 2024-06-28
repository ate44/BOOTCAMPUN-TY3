using UnityEngine;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    public Sprite soundOnSprite;  // Sesi a�ma g�rseli
    public Sprite soundOffSprite; // Sesi kapatma g�rseli
    private Button button; // Buton referans�
    private Image buttonImage; // Butonun Image bile�eni

    private void Start()
    {
        button = GetComponent<Button>();
        buttonImage = button.GetComponent<Image>();

        // �lk durumu ayarla
        UpdateButtonImage();

        // Buton t�klama olay�n� dinle
        button.onClick.AddListener(ToggleSound);
    }

    private void ToggleSound()
    {
        AudioSource audioSource = AudioManager.Instance.audioSource;

        if (audioSource.isPlaying)
        {
            audioSource.Pause(); // M�zi�i duraklat
        }
        else
        {
            audioSource.Play(); // M�zi�i �al
        }

        UpdateButtonImage(); // G�rseli g�ncelle
    }

    private void UpdateButtonImage()
    {
        AudioSource audioSource = AudioManager.Instance.audioSource;

        if (audioSource.isPlaying)
        {
            buttonImage.sprite = soundOnSprite; // Sesi a�ma g�rseli
        }
        else
        {
            buttonImage.sprite = soundOffSprite; // Sesi kapatma g�rseli
        }
    }
}
