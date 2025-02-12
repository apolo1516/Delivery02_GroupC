using UnityEngine;
using UnityEngine.UIElements;

public class EnemyChase : MonoBehaviour
{
    GameObject player;
    private float speed;
    private Rigidbody2D _rigidbody;

    void Start()
    {   
        player = GameObject.Find("Player");
        speed = 200f;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }
    void Follow()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 0.4f)
        {
            return;
        }

        // transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);


        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        Vector2 velocity = dir * speed * Time.deltaTime;
        _rigidbody.linearVelocity = velocity;
        Debug.Log(_rigidbody.linearVelocity);

        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }
}
