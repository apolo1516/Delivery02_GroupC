using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    private Transform targetPoint;

    public Transform detectionPoint; 
    public float visionRange = 5f;
    public float visionAngle = 45f;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    public GameObject alarmIndicator; 
    private bool playerDetected = false;

    private Transform player;
    private Vector3 originalScale;

    void Start()
    {
        targetPoint = pointA;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalScale = transform.localScale;
    }

    void Update()
    {
        Patrol();
        DetectPlayer();
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
            Flip();
        }
    }

    void Flip()
    {
        Vector3 newScale = originalScale;
        newScale.x *= -1;
        transform.localScale = newScale;
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
                    playerDetected = true;
                    alarmIndicator.SetActive(true);
                    return;
                }
            }
        }

        playerDetected = false;
        alarmIndicator.SetActive(false);
    }

    void OnDrawGizmos()
    {
        if (detectionPoint == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(detectionPoint.position, visionRange);

        Vector3 rightLimit = Quaternion.Euler(0, 0, visionAngle / 2) * transform.right * visionRange;
        Vector3 leftLimit = Quaternion.Euler(0, 0, -visionAngle / 2) * transform.right * visionRange;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(detectionPoint.position, detectionPoint.position + rightLimit);
        Gizmos.DrawLine(detectionPoint.position, detectionPoint.position + leftLimit);

        if (player != null && playerDetected)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(detectionPoint.position, player.position);
        }
    }
}
