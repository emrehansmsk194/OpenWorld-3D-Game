using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMoving : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 2f;
    public float turnSpeed = 20f;
    public GameObject car;
    public bool isGrounded;
    public Rigidbody rb;
    public bool activeCharacterMovement = true;
    CharacterAnimations animations;
  
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animations = GetComponentInChildren<CharacterAnimations>();
        isGrounded = true;
        
    }

    void Update()
    {
        
      
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (activeCharacterMovement)
        {
            // �leri hareket kontrol�
            float currentSpeed = 0f;

            if (z > 0) // W tu�una bas�ld���nda
            {
                currentSpeed = moveSpeed;
            }

            // Yava�lama/Durma kontrol� (S tu�u)
            if (z < 0) // S tu�una bas�ld���nda
            {
                //currentSpeed = Mathf.Max(0, currentSpeed - Time.deltaTime * moveSpeed);
                currentSpeed = -moveSpeed;
            }

            // D�n�� kontrol� (A ve D tu�lar�)
            if (x != 0 && z > 0)
            {
                float rotationStep = x * turnSpeed * Time.deltaTime;
                transform.Rotate(0, rotationStep, 0);
               
            }
            else if(x!=0 && z < 0)
            {
                float rotationStep = -x * turnSpeed * Time.deltaTime;
                transform.Rotate(0, rotationStep, 0);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed *= 4f;
                if(z < 0)
                {
                    currentSpeed *= 1.3f;
                }
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                animations.anim.SetBool("isGrounded", isGrounded);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;

            }
            Vector3 movement = transform.forward * currentSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
            animations.anim.SetBool("isGrounded", isGrounded);
        }
       
       
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts.Length > 0)
        {
            ContactPoint contact = collision.contacts[0];
            if (collision.gameObject.tag == "Ground")
            {
                // E�er temas edilen y�zey yukar� y�nl�yse (yani altta), karakterin zeminde oldu�unu belirt
                isGrounded = true;
            }
        }
    }
    
}