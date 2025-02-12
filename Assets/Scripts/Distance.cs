using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Distance : MonoBehaviour
{
    public Text distanceText;
    GameObject player;
    private Vector2 _position;
    private Vector2 _newPosition;
    private float _distanceMoved;

    void Start()
    {
        player = GameObject.Find("Player");
        _distanceMoved = 0f;
        _position = GetPlayerPosition();
        _newPosition = GetPlayerPosition();
    }

    void Update()
    {
        _newPosition = GetPlayerPosition();

        if (_newPosition != _position )
        {
            _distanceMoved += Vector2.Distance(_position, _newPosition);
        }
        _position = GetPlayerPosition();

        DisplayTime(_distanceMoved);
    }
    public Vector2 GetPlayerPosition()
    {
        return new Vector2(player.transform.position.x, player.transform.position.y);
    }

    void DisplayTime(float dist)
    {
        distanceText.text = string.Format("Distance: " + dist.ToString("F2") + " units"); 
    }
}
