using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMoving : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 20f; // Karakterin dönüþ hýzý
    public float speedForAnim;
    public bool isGrounded;
    public Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero; // Hareket yönünü tutan vektör



    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Ýleri hareket kontrolü
        float currentSpeed = 0f;
        if (z > 0) // W tuþuna basýldýðýnda
        {
            currentSpeed = moveSpeed;
        }

        // Yavaþlama/Durma kontrolü (S tuþu)
        if (z < 0) // S tuþuna basýldýðýnda
        {
            currentSpeed = Mathf.Max(0, currentSpeed - Time.deltaTime * moveSpeed);
        }

        // Dönüþ kontrolü (A ve D tuþlarý)
        if (x != 0)
        {
            float rotationStep = x * turnSpeed * Time.deltaTime;
            transform.Rotate(0, rotationStep, 0);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= 4f;
        }
        // Hareketi uygula
        Vector3 movement = transform.forward * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }
}