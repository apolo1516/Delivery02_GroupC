using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreSystem : MonoBehaviour
{
    public int Score;

    public static ScoreSystem Instance;
    public static Action<int> OnScoreUpdated;
    public AudioClip CoinSound;
    private AudioSource audioSource;

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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Coin.OnCoinCollected += UpdateScore;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= UpdateScore;
    }

    private void UpdateScore(Coin coin)
    {
        Score += coin.Value;
        if (audioSource != null && CoinSound != null)
        {
            audioSource.PlayOneShot(CoinSound);
        }
        OnScoreUpdated?.Invoke(Score);
    }
}