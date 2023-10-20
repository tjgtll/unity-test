using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ElevatorBlock : MonoBehaviour
{
    public Transform topPosition; 
    public Transform bottomPosition; 
    public float speed = 2f;
    bool movingUp = true; 

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        Vector3 targetPosition = movingUp ? topPosition.position : bottomPosition.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == topPosition.position)
        {
            movingUp = false;
        }
        else if (transform.position == bottomPosition.position)
        {
            movingUp = true;
        }
    }

    
}
