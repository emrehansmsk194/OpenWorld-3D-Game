using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CharacterGunController : MonoBehaviour
{
    public GameObject crosshair;
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineComposer composer;
    private bool isAiming = false;
    public GameObject Gun;
    public GameObject AimCamera;
    public Camera camera;
    private Camera mainCamera;
    public float sensitivity = 4f;
    public float range = 100.0f;
    CharacterAnimations animations;
    void Start()
    {
        mainCamera = Camera.main;
        animations = GetComponentInChildren<CharacterAnimations>();
        if(virtualCamera != null)
        {
            composer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();
        }
    }

    
    void Update()
    {
 


        if (Input.GetMouseButtonDown(1))
        {
            isAiming = !isAiming;
        }
        animations.anim.SetBool("aim", isAiming);
        if (isAiming)
        {
            
            Gun.transform.localRotation = Quaternion.Euler(Gun.transform.localRotation.eulerAngles.x, -90, Gun.transform.localRotation.eulerAngles.z);
            AimCamera.SetActive(true);
            crosshair.SetActive(true);


        }
        else
        {
            Gun.transform.localRotation = Quaternion.Euler(Gun.transform.localRotation.eulerAngles.x, 240.747f, Gun.transform.localRotation.eulerAngles.z);
            AimCamera.SetActive(false);
            crosshair.SetActive(false);
            camera.fieldOfView = 23;
        }
        if(composer != null && isAiming)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity *Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity *Time.deltaTime;

            composer.m_TrackedObjectOffset.x += (mouseX / 80);
            composer.m_TrackedObjectOffset.y += (mouseY / 80);
            transform.Rotate(Vector3.up * mouseX * (1/sensitivity) *70);
            // Offset deðerlerini sýnýrlar içinde tut
            composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, -0.9f, 0.9f);
            composer.m_TrackedObjectOffset.x = Mathf.Clamp(composer.m_TrackedObjectOffset.x, 0.9f, 1.7f);
           
        }
        if(isAiming && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        // Kameranýn bulunduðu pozisyondan, kameranýn bakýþ açýsýnýn ileri doðrultusuna doðru bir ray gönderir.
        if (Physics.Raycast(rayOrigin, mainCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name); // Çarptýðý nesnenin adýný konsola yazdýr.

           
        }
    }

}
