using System;
using Unity.Hierarchy;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text timeText; 
    private float startTime;

    public static TimeManager Instance;
    public static Action<int> OnScoreUpdated;
    public float ElapsedTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        ElapsedTime = Time.time - startTime; 
        DisplayTime(ElapsedTime); 

        if (ElapsedTime >= 60)
        {
            SceneManager.LoadScene("Ending");
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeText.text = string.Format("Time: " + (60 - timeToDisplay).ToString("F2")); 
    }
}
