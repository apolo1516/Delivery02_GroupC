using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalStats : MonoBehaviour
{
    private Text _label;
    private void Awake()
    {
        _label = GetComponent<Text>();
    }
    private void Start()
    {
        _label.text = "Score: " + ScoreSystem.Instance.Score.ToString();
    }
}

