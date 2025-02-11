using System;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public Transform detectionPoint;
    public float visionRange = 5f;
    public float visionAngle = 45f;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;
    private Transform player;
    private bool playerDetected = false;
    public event Action OnPlayerDetected;
    public event Action OnPlayerLost;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        if (player == null || detectionPoint == null) return;

        Vector3 directionToPlayer = (player.position - detectionPoint.position).normalized;
        float distanceToPlayer = Vector3.Distance(detectionPoint.position, player.position);

        if (distanceToPlayer <= visionRange)
        {
            float angleToPlayer = Vector3.Angle(transform.right, directionToPlayer);

            if (angleToPlayer < visionAngle / 2)
            {
                if (!Physics2D.Raycast(detectionPoint.position, directionToPlayer, distanceToPlayer, obstacleLayer))
                {
                    if (!playerDetected)
                    {
                        playerDetected = true;
                        OnPlayerDetected?.Invoke();
                    }
                    return;
                }
            }
        }

        if (playerDetected)
        {
            playerDetected = false;
            OnPlayerLost?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        if (detectionPoint == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(detectionPoint.position, visionRange); 

        // Dibuja el cono de visión
        Vector3 rightEdge = Quaternion.Euler(0, 0, visionAngle / 2) * transform.right * visionRange;
        Vector3 leftEdge = Quaternion.Euler(0, 0, -visionAngle / 2) * transform.right * visionRange;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(detectionPoint.position, detectionPoint.position + rightEdge); 
        Gizmos.DrawLine(detectionPoint.position, detectionPoint.position + leftEdge);  

        Gizmos.color = Color.red;
        Gizmos.DrawLine(detectionPoint.position, detectionPoint.position + transform.right * visionRange);
    }
}
