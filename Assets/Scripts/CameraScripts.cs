using UnityEngine;

public class CameraScripts : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public Transform target; // Kameran�n takip etmesi gereken hedef
    public float distanceFromTarget = 10.0f; // Hedefle kamera aras�ndaki mesafe
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Mouse imlecini kilitle ve gizle
    }

    void LateUpdate()
    {
        // Mouse hareketlerini al
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Kameran�n y ekseninde d�n���n� hesapla ve limitler koy
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Kameran�n x ekseninde d�n���n� hesapla
        yRotation += mouseX;

        // Kameran�n rotasyonunu g�ncelle
        transform.LookAt(target);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Vector3 newPosition = target.position - transform.forward * distanceFromTarget;
        newPosition.y = Mathf.Max(newPosition.y, 0.20f);
        transform.position = newPosition;
    }
}

