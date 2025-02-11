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
    }

    void DeactivateAlarm()
    {
        alarmIndicator.SetActive(false);
    }
}
