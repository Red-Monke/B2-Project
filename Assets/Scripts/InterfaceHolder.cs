using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public interface ISwitchable
{
    bool isActive { get; }
    void Activate();
    void Deactivate();
}

public interface IInteractable
{
    void Interact();
}

public interface IDamageable
{
    public int maxHealth { get; set; }
    public int currentHealth { get; set; }
    void SetHealth();
    void TakeDamage(int damageValue, int objectHealthValue);
    void RestoreHealth(int healValue, int objectHealthValue);
    void Die();
}

public interface IHealing
{
    int healValue { get; set; }
    void OnHeal();
}

public interface ICommand
{
    void Execute();
    void Undo();
}

public interface IUsableItem
{
    //use to throw or place item on ground
    public void ShareItem();
    public void UseItem();
}

public interface IBoostHealth
{
    public void IncreaseMaxHealth(int boostAmount, int currentMaxHealth);
}

