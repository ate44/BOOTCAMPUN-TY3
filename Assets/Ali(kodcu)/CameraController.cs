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
        Cursor.lockState = CursorLockMode.Locked;  // Mause imlecini ekran ortas�nda kilitle
        Cursor.visible = false;  // Mause imlecini gizle
    }

    private void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -40f, 85f);  // Kameran�n yukar�-a�a�� a��s�n� s�n�rlama

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);  // Kameran�n a��s�n� belirleme
        Vector3 position = target.position - (rotation * Vector3.forward * distanceFromTarget + offset);  // Kameran�n pozisyonunu belirleme

        transform.position = position;  // Kameray� yeni pozisyona ta��ma
        transform.LookAt(target.position + Vector3.up * 1.5f);  // Kameran�n karakteri takip etmesini sa�lama
    }
}
