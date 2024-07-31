using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ParaSistemi : MonoBehaviour
{
    public int para = 300;
    public TextMeshProUGUI paraText;
    public Button Buttonextradamagepotion;
    public Button Buttonenergypotion;
    public Button Buttonhealthpotion;
    public Button Buttonforceshieldpotion;
    public AudioClip satinAlmaSesi;
    public AudioClip iadeEtmeSesi;
    public AudioClip uyariSesi;

    public int iksirSayisiExtraDamage = 0;
    public int iksirSayisiEnergy = 0;
    public int iksirSayisiHealth = 0;
    public int iksirSayisiShield = 0;
    public int cansayisi = 0;
    public int enerjisayisi = 0;
    public int extrahasar = 0;
    public int kalkan = 0;
    public TextMeshProUGUI iksirTextextradamage;
    public TextMeshProUGUI iksirTextheal;
    public TextMeshProUGUI iksirTextshield;
    public TextMeshProUGUI iksirTextenergy;
    public TextMeshProUGUI cansayisitext;
    public TextMeshProUGUI enerjisayisitext;
    public TextMeshProUGUI extrahasartext;
    public TextMeshProUGUI kalkantext;
    private AudioSource audioSource;

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
        // Referanslarýn doðru þekilde atanýp atanmadýðýný kontrol edin
        if (paraText == null) Debug.LogError("Para Text is not assigned.");
        if (Buttonextradamagepotion == null) Debug.LogError("Extra Damage Potion Button is not assigned.");
        if (Buttonenergypotion == null) Debug.LogError("Energy Potion Button is not assigned.");
        if (Buttonhealthpotion == null) Debug.LogError("Health Potion Button is not assigned.");
        if (Buttonforceshieldpotion == null) Debug.LogError("Force Shield Potion Button is not assigned.");
        if (iksirTextextradamage == null) Debug.LogError("Extra Damage Potion Text is not assigned.");
        if (iksirTextheal == null) Debug.LogError("Health Potion Text is not assigned.");
        if (iksirTextshield == null) Debug.LogError("Shield Potion Text is not assigned.");
        if (iksirTextenergy == null) Debug.LogError("Energy Potion Text is not assigned.");
        if (satinAlmaSesi == null) Debug.LogError("Satýn Alma Sesi is not assigned.");
        if (iadeEtmeSesi == null) Debug.LogError("Ýade Etme Sesi is not assigned.");
        if (uyariSesi == null) Debug.LogError("Uyarý Sesi is not assigned.");

        // Audio Source'u al
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) Debug.LogError("AudioSource is not attached to the GameObject.");

        // Baþlangýçta para ve iksir deðerlerini güncelle
        ParaGuncelle();
        IksirGuncelle();

        // Butonlara týklanma olaylarýný dinle
        Buttonextradamagepotion.onClick.AddListener(IksirKullanExtraDamage);
        Buttonenergypotion.onClick.AddListener(IksirKullanEnergy);
        Buttonhealthpotion.onClick.AddListener(IksirKullanHealth);
        Buttonforceshieldpotion.onClick.AddListener(IksirKullanShield);
    }

    void Update()
    {
        // Sað týklama kontrolü
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Input.mousePosition;
            if (RectTransformUtility.RectangleContainsScreenPoint(Buttonextradamagepotion.GetComponent<RectTransform>(), mousePosition, Camera.main))
            {
                IadeEtExtraDamage();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(Buttonenergypotion.GetComponent<RectTransform>(), mousePosition, Camera.main))
            {
                IadeEtEnergy();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(Buttonhealthpotion.GetComponent<RectTransform>(), mousePosition, Camera.main))
            {
                IadeEtHealth();
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(Buttonforceshieldpotion.GetComponent<RectTransform>(), mousePosition, Camera.main))
            {
                IadeEtShield();
            }
        }

    }

    void IksirKullanExtraDamage()
    {
        if (para >= 100)
        {
            para -= 100;
            iksirSayisiExtraDamage += 1;
            extrahasar += 1;
            ParaGuncelle();
            IksirGuncelle();
            OynatSatinAlmaSesi();
        }
        else
        {
            OynatUyariSesi();
            Debug.Log("Yetersiz para!");
        }
    }

    void IksirKullanEnergy()
    {
        if (para >= 50)
        {
            para -= 50;
            iksirSayisiEnergy += 1;
            enerjisayisi += 1;

            ParaGuncelle();
            IksirGuncelle();
            OynatSatinAlmaSesi();
        }
        else
        {
            OynatUyariSesi();
            Debug.Log("Yetersiz para!");
        }
    }

    void IksirKullanHealth()
    {
        if (para >= 50)
        {
            para -= 50;
            iksirSayisiHealth += 1;
            cansayisi += 1;
            ParaGuncelle();
            IksirGuncelle();
            OynatSatinAlmaSesi();
            Debug.Log("healthkullanýldý " + cansayisi);
        }
        else
        {
            OynatUyariSesi();
            Debug.Log("Yetersiz para!");
        }
    }

    void IksirKullanShield()
    {
        if (para >= 100)
        {
            para -= 100;
            iksirSayisiShield += 1;
            kalkan += 1;
            ParaGuncelle();
            IksirGuncelle();
            OynatSatinAlmaSesi();
        }
        else
        {
            OynatUyariSesi();
            Debug.Log("Yetersiz para!");
        }
    }

   public void KullanExtraDamage()
    {
        if (extrahasar > 0)
        {
            audioManager.PlaySFX(audioManager.usingSpell);
            extrahasar -= 1;
            iksirSayisiExtraDamage -= 1;
            IksirGuncelle();
            Debug.Log("Extra Damage Potion kullanýldý, kalan sayýsý: " + iksirSayisiExtraDamage);
        }
        else
        {
            OynatUyariSesi();
            Debug.Log("Kullanýlacak ekstra hasar iksiri yok!");
        }
    }

   public void KullanEnergy()
    {
        if (enerjisayisi > 0)
        {
            audioManager.PlaySFX(audioManager.usingSpell);

            enerjisayisi -= 1;
            iksirSayisiEnergy -= 1;
            IksirGuncelle();
            Debug.Log("Energy Potion kullanýldý, kalan sayýsý: " + iksirSayisiEnergy);
        }
        else
        {
            OynatUyariSesi();
            Debug.Log("Kullanýlacak enerji iksiri yok!");
        }
    }

  public  void KullanHealth()
    {
        if (cansayisi > 0)
        {
            audioManager.PlaySFX(audioManager.usingSpell);

            cansayisi -= 1;
            iksirSayisiHealth -= 1;
            IksirGuncelle();
            Debug.Log("Health Potion kullanýldý, kalan sayýsý: " + iksirSayisiHealth);
        }
        else
        {
            OynatUyariSesi();
            Debug.Log("Kullanýlacak saðlýk iksiri yok!");
        }
    }

  public  void KullanShield()
    {
        if (kalkan > 0)
        {
            audioManager.PlaySFX(audioManager.usingSpell);

            kalkan -= 1;
            iksirSayisiShield -= 1;
            IksirGuncelle();
            Debug.Log("Shield Potion kullanýldý, kalan sayýsý: " + iksirSayisiShield);
        }
        else
        {
            OynatUyariSesi();
            Debug.Log("Kullanýlacak kalkan iksiri yok!");
        }
    }

    void IadeEtExtraDamage()
    {
        if (iksirSayisiExtraDamage > 0)
        {
            iksirSayisiExtraDamage -= 1;
            para += 100;
            ParaGuncelle();
            IksirGuncelle();
            OynatIadeEtmeSesi();
        }
        else
        {
            OynatUyariSesi();
            Debug.Log("Ýade edilecek ekstra hasar iksiri yok!");
        }
    }

    void IadeEtEnergy()
    {
        if (iksirSayisiEnergy > 0)
        {
            iksirSayisiEnergy -= 1;
            para += 50;
            ParaGuncelle();
            IksirGuncelle();
            OynatIadeEtmeSesi();
        }
        else
        {
            OynatUyariSesi();
            Debug.Log("Ýade edilecek enerji iksiri yok!");
        }
    }

    void IadeEtHealth()
    {
        if (iksirSayisiHealth > 0)
        {
            iksirSayisiHealth -= 1;
            para += 50;
            ParaGuncelle();
            IksirGuncelle();
            OynatIadeEtmeSesi();
        }
        else
        {
            OynatUyariSesi();
            Debug.Log("Ýade edilecek saðlýk iksiri yok!");
        }
    }

    void IadeEtShield()
    {
        if (iksirSayisiShield > 0)
        {
            iksirSayisiShield -= 1;
            para += 100;
            ParaGuncelle();
            IksirGuncelle();
            OynatIadeEtmeSesi();
        }
        else
        {
            OynatUyariSesi();
            Debug.Log("Ýade edilecek kalkan iksiri yok!");
        }
    }

    void ParaGuncelle()
    {
        paraText.text = para.ToString();
    }

    void IksirGuncelle()
    {
        iksirTextextradamage.text = iksirSayisiExtraDamage.ToString();
        iksirTextheal.text = iksirSayisiHealth.ToString();
        iksirTextshield.text = iksirSayisiShield.ToString();
        iksirTextenergy.text = iksirSayisiEnergy.ToString();
        cansayisitext.text = cansayisi.ToString();
        kalkantext.text = kalkan.ToString();
        extrahasartext.text = extrahasar.ToString();
        enerjisayisitext.text = enerjisayisi.ToString();
        Debug.Log("Can sayýsý güncellendi, yeni deðer: " + cansayisi);
    }

    void OynatSatinAlmaSesi()
    {
        if (satinAlmaSesi != null && audioSource != null)
        {
            audioSource.PlayOneShot(satinAlmaSesi);
        }
    }

    void OynatIadeEtmeSesi()
    {
        if (iadeEtmeSesi != null && audioSource != null)
        {
            audioSource.PlayOneShot(iadeEtmeSesi);
        }
    }

    void OynatUyariSesi()
    {
        if (uyariSesi != null && audioSource != null)
        {
            audioSource.PlayOneShot(uyariSesi);
        }
    }
}
