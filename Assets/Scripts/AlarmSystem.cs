using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    public GameObject alarmIndicator;
    private EnemyVision enemyVision;

    void Start()
    {
        enemyVision = GetComponent<EnemyVision>();
        if (enemyVision != null)
        {
            enemyVision.OnPlayerDetected += ActivateAlarm;
            enemyVision.OnPlayerLost += DeactivateAlarm;
        }
    }

    void ActivateAlarm()
    {
        alarmIndicator.SetActive(true);
        GetComponent<EnemyPatrol>().isPatroling = false;
        GetComponent<EnemyChase>().isChasing = true;
    }

    void DeactivateAlarm()
    {
        alarmIndicator.SetActive(false);
        GetComponent<EnemyPatrol>().isPatroling = true;
        GetComponent<EnemyChase>().isChasing = false;
    }
}
