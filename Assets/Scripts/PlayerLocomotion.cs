using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    Transform cameraObject;
    InputHandler inputHandler;
    Vector3 moveDirection;

    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]
    public AnimatorHandler animatorHandler;

    public new Rigidbody rigidbody;
    public GameObject normalCamera;

    [Header("Stats")]
    [SerializeField]
    float movementSpeed = 5;
    [SerializeField]
    float rotationSpeed = 10;

    [SerializeField] float jumpHeight = 1f;
    [SerializeField] LayerMask groundLayer; 
    bool isGrounded;
    float delay = 0.3f;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        animatorHandler = GetComponent<AnimatorHandler>();
        //Debug.ClearDeveloperConsole();
        //Debug.Log(animatorHandler);

        cameraObject = Camera.main.transform;
        myTransform = transform;
        animatorHandler.Initialize();
    }

    public void Update()
    {
        float delta = Time.deltaTime;

        inputHandler.TickInput(delta);
        isGrounded = Physics.Raycast(myTransform.position, Vector3.down, 0.2f, groundLayer);
        HandleJump();

        HandleMovement(delta);
        HandleRollingAndSprinting(delta);
    }



    #region Movement
    Vector3 normalVector;
    Vector3 targetPosition;
    void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            animatorHandler.PlayTargetAnimation("Jumping", true);
            StartCoroutine(SmoothJump());
           
        }
    }

    IEnumerator SmoothJump()
    {
        float currentHeight = transform.position.y;
        float targetHeight = currentHeight + jumpHeight; 

        yield return new WaitForSeconds(delay);

        float duration = 0.5f; 

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float newHeight = Mathf.Lerp(currentHeight, targetHeight, elapsed / duration);
            transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, targetHeight, transform.position.z);
    }

    private void HandleRotation(float delta)
    {
        Vector3 targetDir = Vector3.zero;
        float moveOverride = inputHandler.moveAmount;

        targetDir = cameraObject.forward * inputHandler.vertical;
        targetDir += cameraObject.right * inputHandler.horizontal;

        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
        {
            targetDir = myTransform.forward;
        }

        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

        myTransform.rotation = targetRotation;
    }

    public void HandleMovement(float delta)
    {
        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;

        float speed = movementSpeed;
        moveDirection *= speed;

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidbody.velocity = projectedVelocity;

        animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);

        if (animatorHandler.canRotate)
        {
            HandleRotation(delta);
        }
    }

    public void HandleRollingAndSprinting(float delta)
    {
        if (animatorHandler.anim.GetBool("IsInteracting"))
        {
            return;
        }

        if (inputHandler.roolFlag)
        {
            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;

            if (inputHandler.moveAmount > 0)
            {
                animatorHandler.PlayTargetAnimation("Rolling", true);
                moveDirection.y = 0;
                Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                myTransform.rotation = rollRotation;
            }
            else
            {
                animatorHandler.PlayTargetAnimation("Backstep", true);
            }
        }
    }

    #endregion
}
