using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPot : MonoBehaviour, IHealing, IUsableItem
{
    public int healValue { get; set; }
    [SerializeField] int healMultiplier;
    [SerializeField] int baseHeal;
    PlayerLife pLife;
    public void OnHeal()
    {
        gameObject.SetActive(false);
        pLife.RestoreHealth(healValue, pLife.currentHealth);
    }

    void Start()
    {
        pLife = FindObjectOfType<PlayerLife>();
        healValue = baseHeal * healMultiplier;
    }

    public void ShareItem()
    {
        throw new System.NotImplementedException();
    }

    public void UseItem()
    {
        OnHeal();
    }
}
