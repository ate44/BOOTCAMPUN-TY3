using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ses türlerini belirten bir enum
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

// Bu scriptin eklendiði GameObject'te AudioSource component'inin bulunmasýný zorunlu kýlar
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    // Ses dosyalarýnýn listesi
    [SerializeField] private AudioClip[] soundList;
    // Singleton instance
    private static SoundManager instance;
    // AudioSource component'i
    private AudioSource audioSource;

    // Awake metodu, instance'ý ayarlar
    private void Awake()
    {
        instance = this;
    }

    // Start metodu, AudioSource component'ini alýr
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Belirtilen sesi çalar
    public static void PlaySound(SoundType soundType, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)soundType], volume);
    }
}
