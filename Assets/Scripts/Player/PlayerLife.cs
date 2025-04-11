using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour, IDamageable
{
    #region Health Related Variables variables
    public int maxHealth { get; set; }
    public int currentHealth { get; set; }
    #endregion

    public void SetHealth()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        Debug.Log("current health = " + currentHealth);
    }

    public void TakeDamage(int damageValue, int objectHealthValue)
    {
        currentHealth = Mathf.Clamp(currentHealth - damageValue, 0, maxHealth);
        Debug.Log("Player Hurt, Current Health = " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(int healValue, int objectHealthValue)
    {
        currentHealth = Mathf.Clamp(currentHealth + healValue, 0, maxHealth);
        Debug.Log("Player Healed, Current Health = " + currentHealth);
    }

    public void Die()
    {
        Debug.Log("Player dead");
    }

    void Start()
    {
        SetHealth();
    }
}
