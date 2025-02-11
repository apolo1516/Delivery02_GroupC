using UnityEngine;
using System;

public class EnemyPatrol : MonoBehaviour
{
    public event Action OnPlayerDetected;
    public event Action OnPlayerLost;
    public event Action OnPatrolPointReached;

    [Header("Patrol Settings")]
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    private Transform targetPoint;
    private bool facingUp = false;
    private bool movingHorizontally;

    void Start()
    {
        targetPoint = pointB;
        movingHorizontally = Mathf.Abs(pointA.position.x - pointB.position.x) > Mathf.Abs(pointA.position.y - pointB.position.y);
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            targetPoint = (targetPoint == pointA) ? pointB : pointA;
            Flip();
            OnPatrolPointReached?.Invoke();
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
}