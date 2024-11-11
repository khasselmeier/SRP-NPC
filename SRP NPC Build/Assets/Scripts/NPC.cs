using UnityEngine;
using UnityEngine.UI;

public abstract class NPC : MonoBehaviour
{
    public float maxHealth = 100f;
    protected float currentHealth;
    public Slider healthBar;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UpdateHealthBar();

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    protected void UpdateHealthBar()
    {
        healthBar.value = currentHealth;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}