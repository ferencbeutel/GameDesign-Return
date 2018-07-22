using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{

    public float maxHealth;
    public bool hasIFrames = false;

    float currentHealth;
    float invincibilitySecondsAfterDamage = 2f;
    float timeStampLastDamageTaken = -2f;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnDeath();
        Destroy(gameObject, 0);
    }

    public abstract void OnDeath();

    public abstract void OnDamageApplied();

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    public void Load(float currentHealth, float maxHealth)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
    }

    public bool IsInvincible()
    {
        float currentTimeStamp = Time.time;
        return hasIFrames && timeStampLastDamageTaken + invincibilitySecondsAfterDamage > currentTimeStamp;
    }

    public void ApplyDamage(float damage)
    {
        if (!IsInvincible())
        {
            currentHealth -= damage;
            timeStampLastDamageTaken = Time.time;
            OnDamageApplied();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void IncreaseMax(float amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;
    }
}
