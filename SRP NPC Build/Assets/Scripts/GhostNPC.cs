using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostNPC : NPC
{
    private float healthFloor = 10f; //health cannot go under 10hp

    public override void TakeDamage(float amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, healthFloor);
        UpdateHealthBar();
    }

    protected override void Die()
    {
        //do nothing since the ghost does not die
    }
}
