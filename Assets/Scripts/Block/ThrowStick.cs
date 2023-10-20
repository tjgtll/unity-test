using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowStick : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float minRotationAngle = 0f; 
    public float maxRotationAngle = 360f;

    private int rotationDirection = 1;
    void Update()
    {
        float newRotation = rotationSpeed * Time.deltaTime * rotationDirection;
        transform.Rotate(Vector3.forward, newRotation);

        float currentRotation = transform.eulerAngles.z;

        if (currentRotation >= maxRotationAngle || currentRotation <= minRotationAngle)
        {
            rotationDirection *= -1;
        }
    }
}
