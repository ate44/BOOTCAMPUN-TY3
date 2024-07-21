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
    public AudioClip uyariSesi;  // Uyar� sesi

    public int iksirSayisiExtraDamage = 0;
    public int iksirSayisiEnergy = 0;
    public int iksirSayisiHealth = 0;
    public int iksirSayisiShield = 0;

    public TextMeshProUGUI iksirTextextradamage;
    public TextMeshProUGUI iksirTextheal;
    public TextMeshProUGUI iksirTextshield;
    public TextMeshProUGUI iksirTextenergy;

    private AudioSource audioSource;

    void Start()
    {
        // Referanslar�n do�ru �ekilde atan�p atanmad���n� kontrol edin
        if (paraText == null) Debug.LogError("Para Text is not assigned.");
        if (Buttonextradamagepotion == null) Debug.LogError("Extra Damage Potion Button is not assigned.");
        if (Buttonenergypotion == null) Debug.LogError("Energy Potion Button is not assigned.");
        if (Buttonhealthpotion == null) Debug.LogError("Health Potion Button is not assigned.");
        if (Buttonforceshieldpotion == null) Debug.LogError("Force Shield Potion Button is not assigned.");
        if (iksirTextextradamage == null) Debug.LogError("Extra Damage Potion Text is not assigned.");
        if (iksirTextheal == null) Debug.LogError("Health Potion Text is not assigned.");
        if (iksirTextshield == null) Debug.LogError("Shield Potion Text is not assigned.");
        if (iksirTextenergy == null) Debug.LogError("Energy Potion Text is not assigned.");
        if (satinAlmaSesi == null) Debug.LogError("Sat�n Alma Sesi is not assigned.");
        if (iadeEtmeSesi == null) Debug.LogError("�ade Etme Sesi is not assigned.");
        if (uyariSesi == null) Debug.LogError("Uyar� Sesi is not assigned.");  // Uyar� sesi kontrol�

        // Audio Source'u al
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) Debug.LogError("AudioSource is not attached to the GameObject.");

        // Ba�lang��ta para ve iksir de�erlerini g�ncelle
        ParaGuncelle();
        IksirGuncelle();

        // Butonlara t�klanma olaylar�n� dinle
        Buttonextradamagepotion.onClick.AddListener(IksirKullanExtraDamage);
        Buttonenergypotion.onClick.AddListener(IksirKullanEnergy);
        Buttonhealthpotion.onClick.AddListener(IksirKullanHealth);
        Buttonforceshieldpotion.onClick.AddListener(IksirKullanShield);
    }

    void Update()
    {
        // Sa� t�klama kontrol�
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
            iksirSayisiExtraDamage++;
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
            iksirSayisiEnergy++;
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
            iksirSayisiHealth++;
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

    void IksirKullanShield()
    {
        if (para >= 100)
        {
            para -= 100;
            iksirSayisiShield++;
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

    void IadeEtExtraDamage()
    {
        if (iksirSayisiExtraDamage > 0)
        {
            iksirSayisiExtraDamage--;
            para += 100;
            ParaGuncelle();
            IksirGuncelle();
            OynatIadeEtmeSesi();
        }
        else
        {
            OynatUyariSesi();
            Debug.Log("�ade edilecek ekstra hasar iksiri yok!");
        }
    }

    void IadeEtEnergy()
    {
        if (iksirSayisiEnergy > 0)
        {
            iksirSayisiEnergy--;
            para += 50;
            ParaGuncelle();
            IksirGuncelle();
            OynatIadeEtmeSesi();
        }
        else
        {
            OynatUyariSesi();
            Debug.Log("�ade edilecek enerji iksiri yok!");
        }
    }

    void IadeEtHealth()
    {
        if (iksirSayisiHealth > 0)
        {
            iksirSayisiHealth--;
            para += 50;
            ParaGuncelle();
            IksirGuncelle();
            OynatIadeEtmeSesi();
        }
        else
        {
            OynatUyariSesi();
            Debug.Log("�ade edilecek sa�l�k iksiri yok!");
        }
    }

    void IadeEtShield()
    {
        if (iksirSayisiShield > 0)
        {
            iksirSayisiShield--;
            para += 100;
            ParaGuncelle();
            IksirGuncelle();
            OynatIadeEtmeSesi();
        }
        else
        {
            OynatUyariSesi();
            Debug.Log("�ade edilecek kalkan iksiri yok!");
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
