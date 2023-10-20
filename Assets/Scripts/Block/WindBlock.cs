using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlock : MonoBehaviour
{
    public float windForce = 10000f;
    public float windChangeInterval = 2f;
    private float timeSinceLastChange = 0f;
    private Vector3 windDirection;
    private List<Vector3> windDirections = new List<Vector3>();
    void Start()
    {
        windDirections.Add(Vector3.right);
        windDirections.Add(Vector3.left);
        windDirections.Add(Vector3.forward);
        windDirections.Add(Vector3.back);
        InvokeRepeating("ChangeWindDirection", 2f, windChangeInterval);
    }

    void Update()
    {
        timeSinceLastChange += Time.deltaTime;

        if (timeSinceLastChange >= windChangeInterval)
        {
            ChangeWindDirection();
            timeSinceLastChange = 0f;
        }
    }

    void ChangeWindDirection()
    {
        windDirection = windDirections[Random.Range(0, windDirections.Count)];

        transform.rotation = Quaternion.LookRotation(-windDirection);
    }

    void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log($"{windDirection} {windForce}");
            rb.AddForce(windDirection * windForce, ForceMode.Force);
        }
    }
}
