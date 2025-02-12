using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    GameObject player;
    private float speed;

    void Start()
    {   
        player = GameObject.Find("Player");
        speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }
    void Follow()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 0.2f)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }
}
