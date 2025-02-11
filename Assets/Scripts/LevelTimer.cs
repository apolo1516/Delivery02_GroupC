using System;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public static LevelTimer Instance { get; private set; } // Singleton

    public event Action<float> OnTimeChanged; // Evento para notificar cambios en el tiempo

    private float _elapsedTime;
    private bool _isLevelCompleted;

    private void Awake()
    {
        // Configurar el Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Update()
    {
        if (!_isLevelCompleted)
        {
            _elapsedTime += Time.deltaTime; // Incrementar el tiempo
            OnTimeChanged?.Invoke(_elapsedTime); // Notificar a los observadores
        }
    }

    public void CompleteLevel()
    {
        _isLevelCompleted = true; // Detener el temporizador al completar el nivel
    }

    public float GetElapsedTime()
    {
        return _elapsedTime; // Obtener el tiempo transcurrido
    }
}