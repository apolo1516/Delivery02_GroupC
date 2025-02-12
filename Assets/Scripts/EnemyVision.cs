using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private LineRenderer lineRenderer;
    public int visionSegments = 20; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
            lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.positionCount = visionSegments + 3; 
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = new Color(1f, 0f, 0f, 0.5f); 
        lineRenderer.endColor = new Color(1f, 0f, 0f, 0.1f);
    }

    void Update()
    {
        DetectPlayer();
        DrawVisionCone();
    }

    void DetectPlayer()
    {
        if (player == null || detectionPoint == null) return;

        Vector3 directionToPlayer = (player.position - detectionPoint.position).normalized;
        float distanceToPlayer = Vector3.Distance(detectionPoint.position, player.position);
        Vector3 forwardDirection = detectionPoint.TransformDirection(Vector3.right);
        float angleToPlayer = Vector3.Angle(forwardDirection, directionToPlayer);

        if (distanceToPlayer <= visionRange && angleToPlayer < visionAngle / 2)
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

        if (playerDetected)
        {
            playerDetected = false;
            OnPlayerLost?.Invoke();
        }
    }

    void DrawVisionCone()
    {
        if (detectionPoint == null) return;

        Vector3 forwardDirection = detectionPoint.TransformDirection(Vector3.right);
        float halfAngle = visionAngle / 2;

        lineRenderer.SetPosition(0, detectionPoint.position); 

        for (int i = 0; i <= visionSegments; i++)
        {
            float angle = -halfAngle + (visionAngle / visionSegments) * i;
            Vector3 direction = Quaternion.Euler(0, 0, angle) * forwardDirection;
            Vector3 point = detectionPoint.position + direction * visionRange;
            lineRenderer.SetPosition(i + 1, point);
        }

        lineRenderer.SetPosition(visionSegments + 2, detectionPoint.position);
    }
}
