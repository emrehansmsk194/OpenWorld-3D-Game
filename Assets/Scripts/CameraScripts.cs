using UnityEngine;

public class CameraScripts : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public Transform target; // Kameranýn takip etmesi gereken hedef
    public float distanceFromTarget = 10.0f; // Hedefle kamera arasýndaki mesafe
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

        // Kameranýn y ekseninde dönüþünü hesapla ve limitler koy
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Kameranýn x ekseninde dönüþünü hesapla
        yRotation += mouseX;

        // Kameranýn rotasyonunu güncelle
        transform.LookAt(target);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Vector3 newPosition = target.position - transform.forward * distanceFromTarget;
        newPosition.y = Mathf.Max(newPosition.y, 0.20f);
        transform.position = newPosition;
    }
}

