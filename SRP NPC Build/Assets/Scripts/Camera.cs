using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The NPC (or player) to follow
    public Vector3 offset = new Vector3(0, 10, -10); // Offset from the target (behind the NPC)

    private void LateUpdate()
    {
        if (target != null)
        {
            // Maintain the offset behind the NPC, adjusted based on the NPC's rotation
            transform.position = target.position + target.rotation * offset;

            // Optionally, the camera can look at the NPC to keep it centered in the view
            transform.LookAt(target);
        }
    }
}