using UnityEngine;

public class TextMovement : MonoBehaviour
{
    // Hareketin hýzýný kontrol etmek için
    public float speed = 1.0f;
    // Hareketin genliðini kontrol etmek için
    public float amplitude = 1.0f;

    // Baþlangýç pozisyonunu saklamak için
    private Vector3 startPos;

    void Start()
    {
        // Objeyi baþlatma pozisyonunda saklýyoruz
        startPos = transform.position;
    }

    void Update()
    {
        // Text objesinin yeni pozisyonunu hesapla
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
