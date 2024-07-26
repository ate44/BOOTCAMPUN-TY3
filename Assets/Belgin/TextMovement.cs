using UnityEngine;

public class TextMovement : MonoBehaviour
{
    // Hareketin h�z�n� kontrol etmek i�in
    public float speed = 1.0f;
    // Hareketin genli�ini kontrol etmek i�in
    public float amplitude = 1.0f;

    // Ba�lang�� pozisyonunu saklamak i�in
    private Vector3 startPos;

    void Start()
    {
        // Objeyi ba�latma pozisyonunda sakl�yoruz
        startPos = transform.position;
    }

    void Update()
    {
        // Text objesinin yeni pozisyonunu hesapla
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
