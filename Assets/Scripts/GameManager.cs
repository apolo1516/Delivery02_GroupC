using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void OnStart()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
