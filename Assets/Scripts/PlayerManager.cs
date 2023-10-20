using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputHandler inputHandler;
    Animator anim;

    void Start()
    {
        inputHandler = GetComponentInChildren<InputHandler>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        inputHandler.isInteracting = anim.GetBool("isInteracting");
        inputHandler.roolFlag = false;
    }
}
