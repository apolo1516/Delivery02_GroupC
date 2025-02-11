using UnityEngine;
using UnityEngine.UI; 

public class UIManager : MonoBehaviour
{
    
    [Header("UI References")]
    [SerializeField] private Text _timeText;
   
    private void OnEnable()
    {
        // Buscar el LevelTimer en la escena usando FindFirstObjectByType
        LevelTimer levelTimer = FindFirstObjectByType<LevelTimer>();

        // Verificar si el LevelTimer existe antes de suscribirse
        if (levelTimer != null)
        {
            levelTimer.OnTimeChanged += UpdateTimeText;
        }
        else
        {
            Debug.LogError("LevelTimer no encontrado en la escena.");
        }
    }

    private void OnDisable()
    {
        // Buscar el LevelTimer en la escena usando FindFirstObjectByType
        LevelTimer levelTimer = FindFirstObjectByType<LevelTimer>();

        // Verificar si el LevelTimer existe antes de desuscribirse
        if (levelTimer != null)
        {
            levelTimer.OnTimeChanged -= UpdateTimeText;
        }
    }

    private void UpdateTimeText(float time)
    {
        // Actualizar el texto de la UI con el tiempo formateado
        if (_timeText != null)
        {
            _timeText.text = $"Time: {time:F2} s";
        }
        else
        {
            Debug.LogError("Time Text no asignado en el UIManager.");
        }
    }
    public void OnQuit()
    {
        Application.Quit();
    }

}