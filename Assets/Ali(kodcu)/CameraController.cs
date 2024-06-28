using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float mouseSensitivity = 100f;
    public float distanceFromTarget = 2f;

    private float pitch = 0f;
    private float yaw = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Mause imlecini ekran ortasýnda kilitle
        Cursor.visible = false;  // Mause imlecini gizle
    }

    private void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -40f, 85f);  // Kameranýn yukarý-aþaðý açýsýný sýnýrlama

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);  // Kameranýn açýsýný belirleme
        Vector3 position = target.position - (rotation * Vector3.forward * distanceFromTarget + offset);  // Kameranýn pozisyonunu belirleme

        transform.position = position;  // Kamerayý yeni pozisyona taþýma
        transform.LookAt(target.position + Vector3.up * 1.5f);  // Kameranýn karakteri takip etmesini saðlama
    }
}
