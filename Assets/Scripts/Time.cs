using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text timeText; 
    private float startTime;
    private bool _isLevelCompleted;


    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime; 
        DisplayTime(elapsedTime); 
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // Ajusta para que no empiece en 0

        int minutes = (int)(timeToDisplay / 60); // minuts
        int seconds = (int)(timeToDisplay % 60); // segons

        timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds); 
    }

    public void CompleteLevel()
    {
        _isLevelCompleted = true; 
    }
}
