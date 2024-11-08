using UnityEngine;
using System.Collections;

public class InteractionHandler : MonoBehaviour
{
    private NPCMovementController npcMovementController;
    private NPCStressManager npcStressManager;
    private bool isInteracting = false;

    private void Start()
    {
        npcMovementController = GetComponent<NPCMovementController>();
        npcStressManager = GetComponent<NPCStressManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isInteracting) return;

        if (other.CompareTag("Villager"))
        {
            StartCoroutine(InteractWithVillager());
        }
        else if (other.CompareTag("Monster"))
        {
            StartCoroutine(ReactToMonster());
        }
    }

    private IEnumerator InteractWithVillager()
    {
        isInteracting = true;
        npcMovementController.StopMovement();  // Stop the NPC's movement
        DialogueManager.Instance.ShowDialogue("Wow, what a wonderful display!", 3f); // Show dialogue for 3 seconds
        Debug.Log("Wow, what a wonderful display!");
        yield return new WaitForSeconds(3f); // Wait for dialogue duration
        isInteracting = false;
        npcMovementController.ResumeMovement();
    }

    private IEnumerator ReactToMonster()
    {
        isInteracting = true;
        npcMovementController.StopMovement(); 
        DialogueManager.Instance.ShowDialogue("AHHH! Run away!", 3f);
        Debug.Log("AHH! Run away!");
        Vector3 runDirection = (transform.position - npcMovementController.GetCurrentWaypointPosition()).normalized * 5f;
        npcMovementController.RunAway(runDirection); // Run away
        npcStressManager.IncreaseStress(22); // Increase stress when encountering something scary
        yield return new WaitForSeconds(.25f);
        isInteracting = false;
        npcMovementController.ResumeMovement();
    }
}