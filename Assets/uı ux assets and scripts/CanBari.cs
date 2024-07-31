using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanBari : MonoBehaviour
{
    public float can, animasyonYavasligi;
    private float maxCan, gercekScale;
    public GameObject bar;
    public Image kirmiziEkran; // K�rm�z� ekran i�in Image bile�eni
    public float kirmiziEkranSuresi = 0.5f; // K�rm�z� ekran�n ne kadar s�rede kaybolaca��
    public float kirmiziEkranAlfa = 0.5f; // K�rm�z� ekran�n alfa de�eri

    void Start()
    {
        maxCan = can;
    }

    void Update()
    {
        gercekScale = can / maxCan;

        Debug.Log(can);
        Debug.Log(gercekScale);

        if (transform.localScale.x > gercekScale)
        {
            transform.localScale = new Vector3(transform.localScale.x - (transform.localScale.x - gercekScale) / animasyonYavasligi, transform.localScale.y, transform.localScale.z);
        }
        if (transform.localScale.x < gercekScale)
        {
            transform.localScale = new Vector3(transform.localScale.x + (gercekScale - transform.localScale.x) / animasyonYavasligi, transform.localScale.y, transform.localScale.z);
        }

        if (can <= 0)
        {
            if (bar != null)
            {
                Destroy(bar);
            }
            SceneManager.LoadScene(3);
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
