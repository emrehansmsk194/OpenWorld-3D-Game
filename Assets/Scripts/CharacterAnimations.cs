using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    public Animator anim;
    public bool isFaster;
    CharacterMoving CharacterMoving;
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        float z = Input.GetAxis("Vertical");
        anim.SetFloat("moveSpeed", z);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isFaster = true;
        }
        else
        {
            isFaster = false;
        }
        anim.SetBool("isFaster", isFaster);
    }
}
