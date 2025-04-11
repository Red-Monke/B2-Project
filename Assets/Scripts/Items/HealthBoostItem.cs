using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoostItem : MonoBehaviour, IHealing, IBoostHealth
{
    public int healValue { get; set; }
    PlayerLife pLife;
    private int boostValue;
    public int boostBase;
    public int boostMultiplier;

    public void IncreaseMaxHealth(int boostAmount, int currentMaxHealth)
    {
        pLife.maxHealth += boostValue;
    }

    public void OnHeal()
    {
        IncreaseMaxHealth(boostValue, pLife.maxHealth);
        pLife.currentHealth += healValue;
        Debug.Log("Player Health Boosted, New Max Health = " + pLife.maxHealth);
        gameObject.SetActive(false);
    }

    void Start()
    {
        pLife = FindObjectOfType<PlayerLife>();
        healValue = 50;
        boostValue = boostBase * boostMultiplier;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Character1") || collision.gameObject.CompareTag("Character2"))
        {
            OnHeal();
        }
    }

}
