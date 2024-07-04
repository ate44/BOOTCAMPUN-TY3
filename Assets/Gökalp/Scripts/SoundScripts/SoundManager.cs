using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ses t�rlerini belirten bir enum
public enum SoundType
{
    FOOTSTEP,
    RUNNING,
    HURT,
    DIE,
    NATURE,
    BACKGROUNDS,
    CAMPFIRE,
    CROWDED,
    MONSTER,
    BREATHING,
    SWORD_SWORD,
    SWORD_SHIELD,
    SWORD_SWING,
}

// Bu scriptin eklendi�i GameObject'te AudioSource component'inin bulunmas�n� zorunlu k�lar
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    // Ses dosyalar�n�n listesi
    [SerializeField] private AudioClip[] soundList;
    // Singleton instance
    private static SoundManager instance;
    // AudioSource component'i
    private AudioSource audioSource;

    // Awake metodu, instance'� ayarlar
    private void Awake()
    {
        instance = this;
    }

    // Start metodu, AudioSource component'ini al�r
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Belirtilen sesi �alar
    public static void PlaySound(SoundType soundType, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)soundType], volume);
    }
}
