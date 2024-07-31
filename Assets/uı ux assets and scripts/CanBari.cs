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
    public Image bar; // Can barý UI elemaný
    public Image kirmiziEkran; // Kýrmýzý ekran için Image bileþeni
    public float kirmiziEkranSuresi = 0.5f; // Kýrmýzý ekranýn ne kadar sürede kaybolacaðý
    public float kirmiziEkranAlfa = 0.5f; // Kýrmýzý ekranýn alfa deðeri

    private AudioManagerSc audioManager;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManagerSc>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene!");
        }
    }

    void Start()
    {
        maxCan = can;
        if (bar != null)
        {
            bar.fillAmount = can / maxCan; // Barýn baþlangýç deðerini ayarla
        }
    }

    void Update()
    {
        gercekScale = can / maxCan;

        if (bar != null)
        {
            // Can oranýný bar'a uygula
            bar.fillAmount = Mathf.Lerp(bar.fillAmount, gercekScale, Time.deltaTime * animasyonYavasligi);
        }

        if (can <= 0)
        {
            audioManager.PlaySFX(audioManager.deathScream);
            if (bar != null)
            {
                Destroy(bar.gameObject); // Bar GameObject'ini yok et
            }
            SceneManager.LoadScene(2); // Oyuncu ölürse sahneyi deðiþtir
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
