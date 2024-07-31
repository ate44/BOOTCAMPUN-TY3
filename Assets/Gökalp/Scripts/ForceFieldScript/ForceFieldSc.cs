using UnityEngine;
using System.Collections;

public class ForceFieldSc : MonoBehaviour
{
    public Material forceFieldMaterial;
    public float dissolveSpeed = 1.0f;
    private float dissolveAmount = 0.0f;
    private bool isActive = false;
    private Coroutine deactivateCoroutine;
    private Coroutine autoDeactivateCoroutine; // Yeni eklenen Coroutine
    [SerializeField] private GameObject prefab;
    private ParaSistemi ps;

    private void Start()
    {
        ps = GetComponent<ParaSistemi>();
    }

    void Update()
    {
        if(ps.kalkan > 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isActive) // Bariyer aktif de�ilse
                {
                    isActive = true;
                    ActivateForceField();
                    ps.KullanShield();
                }
                // Bariyer aktifken bir �ey yap�lmaz
            }
        }
        

        if (isActive)
        {
            dissolveAmount = Mathf.Min(dissolveAmount + dissolveSpeed * Time.deltaTime, 1.0f);
        }
        else if (dissolveAmount > 0.0f) // Dissolve effect while deactivating
        {
            dissolveAmount = Mathf.Max(dissolveAmount - dissolveSpeed * Time.deltaTime, 0.0f);
        }

        forceFieldMaterial.SetFloat("_Dissolve_Amount", dissolveAmount);
    }

    private void ActivateForceField()
    {
        prefab.SetActive(true);
        dissolveAmount = 0.0f;

        // Otomatik devre d��� b�rakma Coroutine'ini ba�lat
        if (autoDeactivateCoroutine != null)
        {
            StopCoroutine(autoDeactivateCoroutine);
        }
        autoDeactivateCoroutine = StartCoroutine(AutoDeactivateForceField());
    }

    private IEnumerator DeactivateForceField()
    {
        // Wait for 3 seconds before starting to dissolve
        yield return new WaitForSeconds(1.0f);

        // Dissolve the force field
        while (dissolveAmount > 0.0f)
        {
            dissolveAmount = Mathf.Max(dissolveAmount - dissolveSpeed * Time.deltaTime, 0.0f);
            forceFieldMaterial.SetFloat("_Dissolve_Amount", dissolveAmount);
            yield return null;
        }

        // Deactivate the force field GameObject
        prefab.SetActive(false);
        deactivateCoroutine = null;
    }

    private IEnumerator AutoDeactivateForceField()
    {
        // Kuvvet alan�n� aktif ettikten 5 saniye sonra devre d��� b�rak
        yield return new WaitForSeconds(3.0f);
        isActive = false;
        deactivateCoroutine = StartCoroutine(DeactivateForceField());
        autoDeactivateCoroutine = null;
    }
}
