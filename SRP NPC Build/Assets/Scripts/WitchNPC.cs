using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchNPC : NPC
{
    public float regenRate = 20f;

    private void Update()
    {
        RegenerateHealth();
    }

    private void RegenerateHealth()
    {
        currentHealth = Mathf.Min(currentHealth + regenRate * Time.deltaTime, maxHealth);
        UpdateHealthBar();
    }
}
