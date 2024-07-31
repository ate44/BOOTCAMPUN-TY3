using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanBari : MonoBehaviour
{
    public float can = 100f;
    public float animasyonYavasligi = 5f;
    private float maxCan;
    private float gercekScale;
    public Image bar; // Can bar� UI eleman�
    public Image kirmiziEkran; // K�rm�z� ekran i�in Image bile�eni
    public float kirmiziEkranSuresi = 0.5f; // K�rm�z� ekran�n ne kadar s�rede kaybolaca��
    public float kirmiziEkranAlfa = 0.5f; // K�rm�z� ekran�n alfa de�eri

    void Start()
    {
        maxCan = can;
        if (bar != null)
        {
            bar.fillAmount = can / maxCan; // Bar�n ba�lang�� de�erini ayarla
        }
    }

    void Update()
    {
        gercekScale = can / maxCan;

        if (bar != null)
        {
            // Can oran�n� bar'a uygula
            bar.fillAmount = Mathf.Lerp(bar.fillAmount, gercekScale, Time.deltaTime * animasyonYavasligi);
        }

        if (can <= 0)
        {
            if (bar != null)
            {
                Destroy(bar.gameObject); // Bar GameObject'ini yok et
            }
            SceneManager.LoadScene(3); // Oyuncu �l�rse sahneyi de�i�tir
        }

        if (can > maxCan)
        {
            can = maxCan;
        }

        if (can < 0)
        {
            can = 0;
        }
    }

    public void HasarAlindi()
    {
        StopAllCoroutines();
        StartCoroutine(KirmiziEkranEfekti());
    }

    IEnumerator KirmiziEkranEfekti()
    {
        if (kirmiziEkran != null)
        {
            Color renk = kirmiziEkran.color;
            renk.a = kirmiziEkranAlfa;
            kirmiziEkran.color = renk;

            yield return new WaitForSeconds(kirmiziEkranSuresi);

            while (kirmiziEkran.color.a > 0)
            {
                renk.a -= Time.deltaTime / kirmiziEkranSuresi;
                kirmiziEkran.color = renk;
                yield return null;
            }
        }
    }
}
