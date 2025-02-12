using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool IsMoving => _isMoving;

    [SerializeField]
    private float Speed = 5.0f;

    private bool _isMoving;
    Rigidbody2D _rigidbody;
    private Vector2 _position;
    private Vector2 _newPosition;
    private float _distanceMoved;

    private int score = 2000;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _position = GetPlayerPosition();
        _newPosition = GetPlayerPosition();
        _distanceMoved = 0;
    }

    public void OnMove(InputValue value)
    {
        // Read value from control, the type depends on what
        // type of controls the action is bound to
        var inputVal = value.Get<Vector2>();

        Vector2 velocity = inputVal * Speed;
        _rigidbody.linearVelocity = velocity;

        _isMoving = (velocity.magnitude > 0.01f);

        if (_isMoving) LookAt((Vector2)transform.position + velocity);
        else transform.rotation = Quaternion.identity;

        _newPosition = GetPlayerPosition();
        _distanceMoved += Vector2.Distance(_position, _newPosition);
        _position = GetPlayerPosition();
        Debug.Log(_distanceMoved);
    }

    public Vector2 GetPlayerPosition()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }

    // NOTE: InputSystem: "SaveScore" action becomes "OnSaveScore" method
    public void OnSaveScore()
    {
        // Usage example on how to save score
        PlayerPrefs.SetInt("Score", score);
        score = PlayerPrefs.GetInt("Score");
    }

    private void LookAt(Vector2 targetPosition)
    {
        float angle = 0.0f;
        Vector3 relative = transform.InverseTransformPoint(targetPosition);
        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle);
    }
}
