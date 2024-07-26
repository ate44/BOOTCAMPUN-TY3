using System.Collections;
using UnityEngine;

public class ForceFieldController : MonoBehaviour
{
    public Material forceFieldMaterial; // Force field materyalini buraya atay�n
    public string shaderProperty = "_Intensity"; // Shader Graph'teki property ismi
    public float maxIntensity = 1f; // Maksimum intensity de�eri
    public float transitionTime = 1f; // Yaratma ve bozunma s�resi
    public float duration = 5f; // Bariyerin etkin kalma s�resi

    private Coroutine currentCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // B tu�una bas�ld���nda
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(ActivateForceField());
        }
    }

    private IEnumerator ActivateForceField()
    {
        // Yaratma efekti
        yield return StartCoroutine(ChangeIntensity(0f, maxIntensity, transitionTime));

        // Belirli bir s�re boyunca etkin kal
        yield return new WaitForSeconds(duration);

        // Bozunma efekti
        yield return StartCoroutine(ChangeIntensity(maxIntensity, 0f, transitionTime));
    }

    private IEnumerator ChangeIntensity(float from, float to, float time)
    {
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float currentIntensity = Mathf.Lerp(from, to, elapsedTime / time);
            forceFieldMaterial.SetFloat(shaderProperty, currentIntensity);
            yield return null;
        }
        forceFieldMaterial.SetFloat(shaderProperty, to);
    }
}
