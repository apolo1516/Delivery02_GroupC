using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FinalDistance : MonoBehaviour
{
    private Text _label;

    private void Awake()
    {
        _label = GetComponent<Text>();
    }
    private void Start()
    {
        _label.text = "Distance: " + Distance.Instance._distanceMoved.ToString();
    }
}
