using UnityEngine;
using System;

public class EnemyPatrol : MonoBehaviour
{
    public event Action OnPlayerDetected;
    public event Action OnPlayerLost;
    public event Action OnPatrolPointReached;

    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    private Transform targetPoint;
    public bool isPatroling;
    private Rigidbody2D _rigidbody;

    void Start()
    {
        targetPoint = pointB;
        isPatroling = true;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isPatroling) Patrol();
    }

    void Patrol()
    {
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            targetPoint = (targetPoint == pointA) ? pointB : pointA;
            OnPatrolPointReached?.Invoke();
        }
        else
        {
            Vector3 dir = targetPoint.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            dir.Normalize();
            Vector2 velocity = dir * speed;
            _rigidbody.linearVelocity = velocity;
            Debug.Log(_rigidbody.linearVelocity);

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}