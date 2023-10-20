using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPlatform : MonoBehaviour
{
    public GameManager gameManager;

    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (gameManager != null)
            {
                gameManager.LevelCompleted();
            }
        }
    }
}
