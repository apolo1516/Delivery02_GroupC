using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnStart()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
