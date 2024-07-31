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
        // Tek bir AudioManager nesnesi olu�turuldu�unda di�er sahnelere aktar�l�r.
        DontDestroyOnLoad(gameObject);

        // audioManager nesnesini bulmak i�in sahnede zaten ba�ka bir AudioManager var m� diye kontrol edin.
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            // E�er ba�ka bir AudioManager nesnesi varsa, bu nesneyi yok edin.
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


    
}
