using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public float damageAmount = 10f;
    private List<NPC> npcs;

    private void Start()
    {
        npcs = new List<NPC>(FindObjectsOfType<NPC>()); //find the NPCs in the scene and add them to the list
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ApplyDamageToAllNPCs(damageAmount);
        }
    }

    private void ApplyDamageToAllNPCs(float damage)
    {
        foreach (NPC npc in npcs)
        {
            // dont apply damage to PumpkinNPC
            if (npc is PumpkinNPC)
                continue;

            npc.TakeDamage(damage);
        }
    }
}
