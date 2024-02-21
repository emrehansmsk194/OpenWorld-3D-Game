using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterAndExitTheCar : MonoBehaviour
{
    public Transform car;
    public Transform Player;
    public bool isNearCar;
    public bool isDriving;
    public float detectionRadius = 2f;
    public LayerMask playerLayer;
    public GameObject virtualCamera;
    public Camera camera;
    RacingCarScript carScript;
    void Start()
    {
       carScript = GetComponent<RacingCarScript>();
        
    }

    void Update()
    {
        isNearCar = Physics.CheckSphere(transform.position, detectionRadius, playerLayer);
        if(isNearCar && Input.GetKeyDown(KeyCode.E))
        {
            EnterCar();
        }
        if(isDriving && Input.GetKeyDown(KeyCode.Q))
        {
            ExitCar();
        }
        


    }

    void EnterCar()
    {
        isDriving = true;
        Player.transform.SetParent(car);
        Player.gameObject.SetActive(false);
        virtualCamera.SetActive(true);
        carScript.activeControl = true;
    }
    void ExitCar()
    {
        Player.transform.SetParent(null);
        Player.gameObject.SetActive(true);
        virtualCamera.SetActive(false);
        carScript.activeControl = false;
        isDriving = false;
        camera.fieldOfView = 23;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
