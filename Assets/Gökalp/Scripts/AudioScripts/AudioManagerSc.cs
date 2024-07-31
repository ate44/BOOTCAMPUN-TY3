using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerSc : MonoBehaviour
{
    [Header("------- Audio Source ------")]
    [SerializeField] AudioSource m_Source;
    [SerializeField] AudioSource SFXSource;

    [Header("------- Audio Clip ------")]
    public AudioClip mainBackground;
    public AudioClip combatBackground;
    public AudioClip birdSound;
    public AudioClip deathScream;
    public AudioClip gameOver;
    public AudioClip humanSound;
    public AudioClip running;
    public AudioClip monsterScream;
    public AudioClip breathing;
    public AudioClip swordHitting;
    public AudioClip swordSwing;
    public AudioClip wounded;
    public AudioClip walking;
    public AudioClip usingSpell;

    private void Awake()
    {
        // Ensure a single instance of AudioManager persists across scenes.
        DontDestroyOnLoad(gameObject);

        // Check if another AudioManager exists in the scene.
        if (FindObjectsOfType<AudioManagerSc>().Length > 1)
        {
            // Destroy this instance if another AudioManager already exists.
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        m_Source.clip = mainBackground;
        m_Source.Play();
        DontDestroyOnLoad(m_Source);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public bool IsPlaying(AudioClip clip)
    {
        // Check if the provided clip is currently playing on the SFXSource.
        return SFXSource.isPlaying && SFXSource.clip == clip;
    }

    public void StopSFX(AudioClip clip)
    {
        // Stop the SFXSource if the provided clip is currently playing.
        if (SFXSource.isPlaying && SFXSource.clip == clip)
        {
            SFXSource.Stop();
        }
    }
}
