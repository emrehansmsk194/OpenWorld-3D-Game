using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Animations.Rigging;

public class CharacterGunController : MonoBehaviour
{
    public MultiAimConstraint[] multiAimConstraint;
    public Rig aimingRig;
    public GameObject crosshair;
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineComposer composer;
    private bool isAiming = false;
    public bool isFiring = true;
    public GameObject Gun;
    public GameObject AimCamera,aim;
    public Camera camera;
    public Transform rightHand,aimPosition;
    private Camera mainCamera;
    public float sensitivity = 4f;
    public float range = 100.0f;
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;
    CharacterAnimations animations;
    WeaponScripts weapon;
    private RigBuilder rigBuilder;
    void Start()
    {
        rigBuilder = GetComponent<RigBuilder>();
        aimPosition.gameObject.SetActive(false);
        rigBuilder.enabled = false;
        mainCamera = Camera.main;
        animations = GetComponentInChildren<CharacterAnimations>();
        if(virtualCamera != null)
        {
            composer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();
        }
        weapon = GetComponentInChildren<WeaponScripts>();
    }

    
    void Update()
    {
        

        if (animations.isFaster && !isAiming)
        {
            multiAimConstraint[0].weight = 0f;
        }
        else if(!animations.isFaster && !isAiming)
        {
            multiAimConstraint[0].weight = 1f;
        }

        if (Input.GetMouseButtonDown(1))
        {
            isAiming = !isAiming;
            UpdateAimConstraintWeight(isAiming);
        }
        animations.anim.SetBool("aim", isAiming);
        if (isAiming)
        {
            
            Gun.transform.localRotation = Quaternion.Euler(Gun.transform.localRotation.eulerAngles.x, -90, Gun.transform.localRotation.eulerAngles.z);
            AimCamera.SetActive(true);
            crosshair.SetActive(true);
            aimPosition.gameObject.SetActive(true);
            UpdateAimPosition();
          

        }
        else
        {
            Gun.transform.localRotation = Quaternion.Euler(Gun.transform.localRotation.eulerAngles.x, 240.747f, Gun.transform.localRotation.eulerAngles.z);
            AimCamera.SetActive(false);
            crosshair.SetActive(false);
            camera.fieldOfView = 23;
            //aimingRig.weight = 0;
            //aimingRig.GetComponentInChildren<MultiAimConstraint>().weight = 0;
            aimPosition.gameObject.SetActive(false);
        }
        if(composer != null && isAiming)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity *Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity *Time.deltaTime;

            composer.m_TrackedObjectOffset.x += (mouseX / 70);
            composer.m_TrackedObjectOffset.y += (mouseY / 70);

            transform.Rotate(Vector3.up * mouseX * (1/sensitivity) *70);
            // Offset deðerlerini sýnýrlar içinde tut
            composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, -0.5f, 1.5f);
            composer.m_TrackedObjectOffset.x = Mathf.Clamp(composer.m_TrackedObjectOffset.x, 0.7f, 1.5f);
           
        }
        if(isAiming && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        else if(isAiming && Input.GetButtonUp("Fire1"))
        {
            StopShoot();
        }
        
        
    }
    void Shoot()
    {
        isFiring = true;
        Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        foreach(var particle in muzzleFlash)
        {
            particle.Emit(1);
        }
        // Kameranýn bulunduðu pozisyondan, kameranýn bakýþ açýsýnýn ileri doðrultusuna doðru bir ray gönderir.
        if (Physics.Raycast(rayOrigin, mainCamera.transform.forward, out hit, range))
        {
            //Debug.Log("Hit: " + hit.collider.name);
            //Debug.DrawLine(rayOrigin, hit.point, Color.red, 5.0f);
            hitEffect.transform.position = hit.point;
            hitEffect.transform.forward = hit.normal;
            hitEffect.Emit(1);

           
        }
    }
    void StopShoot()
    {
        isFiring = false;
    }
    void UpdateAimPosition()
    {
        RaycastHit hit;
        Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(rayOrigin, mainCamera.transform.forward, out hit, range))
        {
            
            aimPosition.position = hit.point;
        }
        else
        {
            
            aimPosition.position = rayOrigin + (mainCamera.transform.forward * range);
        }
    }
    void UpdateAimConstraintWeight(bool isAiming)
    {
        multiAimConstraint[0].weight = isAiming ? 1f : 0f;
        multiAimConstraint[1].weight = isAiming ? 0.8f : 0f;
        rigBuilder.enabled = isAiming;
    }

}
