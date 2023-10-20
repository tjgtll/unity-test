using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStick : MonoBehaviour
{
    public float rotationSpeed = 50f; 
    public Vector3 rotationAxis; 

    void Update()
    {
        float currentRotation = transform.rotation.eulerAngles.y;
        float newRotation = currentRotation + rotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(rotationAxis * newRotation);
    }
}
