using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RobotVision : MonoBehaviour
{
    [Header("Vision")]
    public float detectionRadius = 8f;
    [Range(0f, 360f)] public float viewAngle = 90f; // FOV cone
    public LayerMask playerMask;      // layer that contains the Player
    public LayerMask obstacleMask;    // walls, obstacles that block sight
    public Transform eyePoint;        // where ray is cast from (head). If null it uses transform

    [Header("Performance")]
    public float checkInterval = 0.15f; // how often (seconds) to check for player

    private float lastCheckTime = 0f;
    private Transform playerTransform;

    void Start()
    {
        if (eyePoint == null) eyePoint = this.transform;
    }

    void Update()
    {
        if (Time.time - lastCheckTime < checkInterval) return;
        lastCheckTime = Time.time;
        CheckForPlayer();
    }

    void CheckForPlayer()
    {
        Debug.Log("RobotVision: Performing detection...");
        // 1) Find players in radius (fast)
        Collider[] hits = Physics.OverlapSphere(eyePoint.position, detectionRadius, playerMask);
        if (hits.Length == 0) return;
        
        // If multiple players or objects, iterate
        foreach (var hit in hits)
        {
            Debug.Log("RobotVision: Performing detection...");
            Transform t = hit.transform;
            Vector3 dirToPlayer = (t.position - eyePoint.position).normalized;

            // 2) Field of view check (optional)
            float angle = Vector3.Angle(eyePoint.forward, dirToPlayer);
            if (angle > viewAngle * 0.5f) continue;

            // 3) Line of sight: raycast and see what we hit first
            float distance = Vector3.Distance(eyePoint.position, t.position);
            RaycastHit rayHit;
            // Note: combine obstacleMask and playerMask if necessary for single ray - here we check obstacles first
            if (Physics.Raycast(eyePoint.position, dirToPlayer, out rayHit, distance, obstacleMask))
            {
                // Something (wall/obstacle) blocks the view
                // rayHit.collider is the obstacle
                // Debug.Log("Blocked by: " + rayHit.collider.name);
                continue;
            }

            // No obstacle hit before reaching player — player is visible
            OnPlayerSeen(t.gameObject);
            return; // stop after seeing a player (or remove return if you want multiple)
        }
    }

    void OnPlayerSeen(GameObject player)
    {
        Debug.Log($"{name} saw the player: {player.name}");
        // TODO: trigger game over, start chase, alert state, etc.
    }

    // Gizmos to help debug in editor
    void OnDrawGizmosSelected()
    {
        Transform eye = (eyePoint != null) ? eyePoint : transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(eye.position, detectionRadius);

        // draw FOV lines
        Vector3 forward = eye.forward;
        Quaternion leftRot = Quaternion.Euler(0, -viewAngle * 0.5f, 0);
        Quaternion rightRot = Quaternion.Euler(0, viewAngle * 0.5f, 0);
        Vector3 leftDir = leftRot * forward;
        Vector3 rightDir = rightRot * forward;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(eye.position, eye.position + leftDir * detectionRadius);
        Gizmos.DrawLine(eye.position, eye.position + rightDir * detectionRadius);
    }
}
