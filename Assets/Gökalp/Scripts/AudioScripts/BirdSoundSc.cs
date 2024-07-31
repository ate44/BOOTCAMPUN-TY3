using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSoundSc : MonoBehaviour
{
    private AudioSource audioSource;
    public float fadeDuration = 2.0f; // Fade in/out süresi

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not found on the Town GameObject!");
        }
        else
        {
            audioSource.volume = 0f; // Sesin baþlangýç ses seviyesi sýfýr
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeIn());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeIn()
    {
        float targetVolume = 0.6f;
        float startVolume = audioSource.volume;

        for (float t = 0; t <= fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    private IEnumerator FadeOut()
    {
        float targetVolume = 0f;
        float startVolume = audioSource.volume;

        for (float t = 0; t <= fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }
}
