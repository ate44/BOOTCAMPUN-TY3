using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Scene management kütüphanesi eklendi

public class CanBari : MonoBehaviour
{
    public float can, animasyonYavasligi;
    private float maxCan, gercekScale;
    public GameObject bar; // bar adlý GameObject'i referans olarak ekleyin

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

        // Can deðeri sýfýr olduðunda sahne deðiþimi ve bar GameObject'ini yok etme
        if (can <= 0)
        {
            if (bar != null)
            {
                Destroy(bar); // bar GameObject'ini yok et
            }
            SceneManager.LoadScene(3);
        }

        if (Input.GetKeyDown("a") && can > 0)
        {
            can -= 10;
        }

        if (can < 0)
        {
            can = 0;
        }
        if (Input.GetKeyDown("s") && can > 0)
        {
            can -= 5;
        }
        if (Input.GetKeyDown("d") && can > 0)
        {
            can -= 20;
        }
        if (can > maxCan)
        {
            can = maxCan;
        }
        if (Input.GetKeyDown("h") && can < maxCan)
        {
            can += 20;
        }
    }
}
