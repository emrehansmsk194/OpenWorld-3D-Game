using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldCarScripts : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private bool isBraking;
    private float brakeForce;
    private float motorTorque;

    [SerializeField] private float maxSteerAngle = 30f;
    [SerializeField] private float motorForce = 150f;
    [SerializeField] private float brakeTorque = 300f;

    [SerializeField] private WheelCollider[] wheelColliders = new WheelCollider[4];
    [SerializeField] private Transform[] wheelTransforms = new Transform[4];


    private float currentSpeed; // Arabanýn mevcut hýzýný tutacak deðiþken
    private float maxSpeed = 2f; // Arabanýn ulaþabileceði maksimum hýz
    private void Start()
    {
        ResetWheelAngles();
    }
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        Debug.Log(currentSpeed);
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isBraking = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isBraking = false;
        }
    }

    private void HandleMotor()
    {
        // Mevcut hýzý km/sa olarak hesapla
        currentSpeed = wheelColliders[0].attachedRigidbody.velocity.magnitude * 0.1f;

        // Motor torkunu ve fren kuvvetini hesapla
        motorTorque = 0; // Motor torkunu varsayýlan olarak sýfýrla
        if (!isBraking)
        {
            if (verticalInput > 0) // W tuþuna basýldýðýnda
            {
                if (currentSpeed < maxSpeed)
                {
                    motorTorque = verticalInput * motorForce; // Hýzlan
                }
            }
            else if (verticalInput < 0) // S tuþuna basýldýðýnda
            {
                if (currentSpeed > 0)
                {
                    motorTorque = verticalInput * motorForce *0.4f; // Yavaþla
                }
                else
                {
                    motorTorque = verticalInput * motorForce *0.2f; // Geri git
                }
            }
        }

        // Fren kuvvetini uygula
        brakeForce = isBraking ? brakeTorque : 0f;

        // Tüm tekerleklere motor torkunu ve fren kuvvetini uygula
        foreach (WheelCollider wheel in wheelColliders)
        {
            wheel.motorTorque = motorTorque;
            wheel.brakeTorque = brakeForce;
        }
    }



    private void HandleSteering()
    {
        float steerAngle = maxSteerAngle * horizontalInput;
        // Tekerlek açýlarýný güncelle
        UpdateSteeringAngle(steerAngle);
    }

    private void UpdateSteeringAngle(float angle)
    {
        wheelColliders[0].steerAngle = angle;
        wheelColliders[1].steerAngle = angle;
    }

    private void ResetWheelAngles()
    {
        // Tekerlek açýlarýný sýfýrla
        UpdateSteeringAngle(0f);
    }

    private void UpdateWheels()
    {
        for (int i = 0; i < 4; i++)
        {
            UpdateSingleWheel(wheelColliders[i], wheelTransforms[i]);
        }
    }

    private void UpdateSingleWheel(WheelCollider collider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}

