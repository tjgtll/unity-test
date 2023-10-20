using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class StartPlatform : MonoBehaviour
{
    public GameManager gameManager;

    void Start()
    {
       
    }

    void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (gameManager != null)
            {
                gameManager.StartTimer();
            }
        }
    }
}
