using UnityEngine;
using System;

public class EnemyPatrol : MonoBehaviour
{
    public event Action OnPlayerDetected;
    public event Action OnPlayerLost;

    [Header("Patrol Settings")]
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    private Transform targetPoint;
    private bool facingUp = false;
    private bool movingHorizontally;

    [Header("Vision Settings")]
    public Transform detectionPoint;
    public float visionRange = 5f;
    public float visionAngle = 45f;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    [Header("Alarm System")]
    public GameObject alarmIndicator;

    private Transform player;
    private bool playerDetected = false;

    void Start()
    {
        targetPoint = pointB;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        movingHorizontally = Mathf.Abs(pointA.position.x - pointB.position.x) > Mathf.Abs(pointA.position.y - pointB.position.y);
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
            targetPoint = (targetPoint == pointA) ? pointB : pointA;
            Flip();
        }
    }

    void Flip()
    {
        if (movingHorizontally)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
        else
        {
            facingUp = !facingUp;
            Vector3 newScale = transform.localScale;
            newScale.y *= -1;
            transform.localScale = newScale;
        }
    }

    void DetectPlayer()
    {
        if (player == null || detectionPoint == null) return;

        Vector3 directionToPlayer = (player.position - detectionPoint.position).normalized;
        float distanceToPlayer = Vector3.Distance(detectionPoint.position, player.position);

        if (distanceToPlayer <= visionRange)
        {
            float angleToPlayer = Vector3.Angle(movingHorizontally ? transform.right : (facingUp ? Vector3.up : Vector3.down), directionToPlayer);

            if (angleToPlayer < visionAngle / 2)
            {
                if (!Physics2D.Raycast(detectionPoint.position, directionToPlayer, distanceToPlayer, obstacleLayer))
                {
                    if (!playerDetected)
                    {
                        playerDetected = true;
                        OnPlayerDetected?.Invoke();
                        alarmIndicator.SetActive(true);
                    }
                    return;
                }
            }
        }

        if (playerDetected)
        {
            playerDetected = false;
            OnPlayerLost?.Invoke();
            alarmIndicator.SetActive(false);
        }
    }

    void OnDrawGizmos()
    {
        if (detectionPoint == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(detectionPoint.position, visionRange);

        Vector3 forwardDirection = movingHorizontally ? transform.right : (facingUp ? Vector3.up : Vector3.down);
        Vector3 upLimit = Quaternion.Euler(0, 0, visionAngle / 2) * forwardDirection * visionRange;
        Vector3 downLimit = Quaternion.Euler(0, 0, -visionAngle / 2) * forwardDirection * visionRange;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(detectionPoint.position, detectionPoint.position + upLimit);
        Gizmos.DrawLine(detectionPoint.position, detectionPoint.position + downLimit);
    }
}
