using Unity.Hierarchy;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text timeText; 
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime; 
        DisplayTime(elapsedTime); 

        if (elapsedTime >= 60)
        {
            SceneManager.LoadScene("Ending");
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeText.text = string.Format("Time: " + (60 - timeToDisplay).ToString("F2")); 
    }
}
