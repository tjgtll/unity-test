using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBlock: MonoBehaviour
{
    private MeshRenderer rend;
    private bool isActivated = false;
    public Material originalColor;
    public Material activationColor;
    public Material damagelColor;
    public float damageDelay = 100f;
    public float resetDelay = 5f;
    public int damageAmount = 1;
    public HealthSystem playerHealth;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActivated)
        {
            isActivated = true;
            StartCoroutine(ActivateTrap(other.gameObject));
        }
    }

    IEnumerator ActivateTrap(GameObject player)
    {
        rend.material = activationColor;
        yield return new WaitForSeconds(damageDelay);

        rend.material = damagelColor;
        if (IsPlayerInsideTrigger(player))
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }

        }
        yield return new WaitForSeconds(0.1f);
        rend.material = activationColor;
        yield return new WaitForSeconds(resetDelay);
        ResetTrap();
    }

    bool IsPlayerInsideTrigger(GameObject player)
    {
        Collider playerCollider = player.GetComponent<Collider>();
        return playerCollider != null && playerCollider.bounds.Intersects(GetComponent<Collider>().bounds);
    }

    void ResetTrap()
    {
        isActivated = false;
        rend.material = originalColor;
    }

}
