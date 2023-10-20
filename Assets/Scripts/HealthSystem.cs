using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public TMP_Text healthText;
    private const string healt = "’œ: ";
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        healthText.text = healt + currentHealth.ToString();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    void Die()
    {
        Debug.Log("Player died!");
    }
}
