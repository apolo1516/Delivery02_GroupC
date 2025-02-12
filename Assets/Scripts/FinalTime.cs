using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalTime : MonoBehaviour
{
    private Text _label;

    private void Awake()
    {
        _label = GetComponent<Text>();
    }
    private void Start()
    {
        _label.text = "Time: " + (60 - TimeManager.Instance.ElapsedTime).ToString("F2") + " seconds left";
    }
}

