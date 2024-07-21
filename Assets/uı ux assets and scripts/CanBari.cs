using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        if (Input.GetKeyDown("a") && can > 0)
        {
            can -= 10;
            HasarAlindi();
        }
        if (Input.GetKeyDown("s") && can > 0)
        {
            can -= 5;
            HasarAlindi();
        }
        if (Input.GetKeyDown("d") && can > 0)
        {
            can -= 20;
            HasarAlindi();
        }
        if (Input.GetKeyDown("h") && can < maxCan)
        {
            can += 20;
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

    void HasarAlindi()
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
