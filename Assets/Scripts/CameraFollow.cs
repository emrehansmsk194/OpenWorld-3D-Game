using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetObject;
    public float smoothFactor = 0.5f; // Smooth Factor will use in Camera rotation
    public Vector3 cameraOffset;
    public bool lookAtTarget = true; // will check that camera looked at on the target or not.

    void Start()
    {
        cameraOffset = transform.position - targetObject.transform.position;
        cameraOffset.x = 0f;

    }

    
    void LateUpdate()
    {
        Vector3 newPosition = targetObject.position + targetObject.rotation * cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
        if (lookAtTarget)
        {
            transform.LookAt(targetObject);
        }
    }
}
