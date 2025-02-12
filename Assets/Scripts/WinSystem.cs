using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSystem : MonoBehaviour
{
    private GameObject scoreManager;
    private void Start()
    {
        scoreManager = GameObject.Find("ScoreSystem");
    }
    private void OnEnable()
    {
        WinObserver.OnWinScreen += WinScreen;
    }

    private void OnDisable()
    {
        WinObserver.OnWinScreen -= WinScreen;
    }

    private void WinScreen(WinObserver win)
    {
        ScoreSystem.Instance.Score = scoreManager.GetComponent<ScoreSystem>().Score;
        SceneManager.LoadScene("WinScreen");
    }
}