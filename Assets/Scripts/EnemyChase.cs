using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EnemyChase : MonoBehaviour
{
    GameObject player;
    private float speed;
    private Rigidbody2D _rigidbody;
    public bool isChasing;
    public AudioClip ChaseSound;
    private AudioSource audioSource;

    void Start()
    {   
        player = GameObject.Find("Player");
        speed = 3f;
        _rigidbody = GetComponent<Rigidbody2D>();
        isChasing = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isChasing)
        {
            audioSource.PlayOneShot(ChaseSound);
            Follow();
        }
        
    }
    void Follow()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 0.4f)
        {
            SceneManager.LoadScene("Ending");
        }

        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        Vector2 velocity = dir * speed;
        _rigidbody.linearVelocity = velocity;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
