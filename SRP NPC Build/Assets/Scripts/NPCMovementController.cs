using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCMovementController : MonoBehaviour
{
    public float walkSpeed = 2f; // Default walking speed
    public List<Transform> waypoints; // List of waypoints to follow
    private int currentWaypointIndex = 0;
    private Rigidbody rb;
    private bool isWaiting = false; // Flag to track waiting state

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // Prevent unwanted rotation

        if (waypoints.Count > 0)
        {
            SetNextTarget();
        }
        else
        {
            Debug.LogWarning("Waypoints list is empty! Ensure waypoints are set in the inspector.");
        }
    }

    private void FixedUpdate()
    {
        if (!isWaiting) // Only move if not waiting
        {
            MoveTowardsTarget();
        }
    }

    private void SetNextTarget()
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            // Log current waypoint index for debugging
            Debug.Log("Moving to waypoint: " + currentWaypointIndex);
            transform.LookAt(waypoints[currentWaypointIndex].position);
        }
        else
        {
            currentWaypointIndex = 0; // Loop back to the first waypoint if all waypoints are visited
            Debug.Log("Looping back to the first waypoint.");
        }
    }

    private void MoveTowardsTarget()
    {
        if (waypoints.Count == 0) return;

        // Move the NPC towards the current waypoint
        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;
        Vector3 movement = new Vector3(direction.x, 0, direction.z);
        rb.velocity = movement * walkSpeed;

        // Check if NPC is close enough to the current waypoint to move to the next one
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.5f)
        {
            StartCoroutine(WaitAtWaypoint()); // Start waiting at the waypoint
        }
    }

    private IEnumerator WaitAtWaypoint()
    {
        isWaiting = true; // Start waiting
        rb.velocity = Vector3.zero; // Stop NPC movement

        // Wait for 2 seconds before moving to the next waypoint
        yield return new WaitForSeconds(2f);

        isWaiting = false; // Stop waiting
        currentWaypointIndex++; // Move to the next waypoint

        if (currentWaypointIndex < waypoints.Count)
        {
            SetNextTarget();
        }
        else
        {
            currentWaypointIndex = 0; // Restart the route
            SetNextTarget();
        }
    }

    public void StopMovement()
    {
        rb.velocity = Vector3.zero; // Stop NPC's movement
    }

    public void ResumeMovement()
    {
        SetNextTarget(); // Restart NPC movement
    }

    public void RunAway(Vector3 runDirection)
    {
        // Calculate the flee direction based on the monster's position
        Vector3 fleeDirection = (transform.position - runDirection).normalized;

        // Apply velocity for running away at the designated run speed
        rb.velocity = fleeDirection * walkSpeed; // Run away with speed of 4
        Debug.Log("Running away from monster! Direction: " + fleeDirection);
    }

    // Get the current waypoint position
    public Vector3 GetCurrentWaypointPosition()
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            return waypoints[currentWaypointIndex].position;
        }
        return transform.position; // If no waypoints, return current position
    }
}