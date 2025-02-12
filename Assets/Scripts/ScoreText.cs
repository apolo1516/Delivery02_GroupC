using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreText : MonoBehaviour
{
    private Text _label;

    private void Awake()
    {
        _label = GetComponent<Text>();
    }

    private void OnEnable()
    {
        ScoreSystem.OnScoreUpdated += UpdateScoreText;
    }

    private void OnDisable()
    {
        ScoreSystem.OnScoreUpdated -= UpdateScoreText;
    }

    private void UpdateScoreText(int score)
    {
        _label.text = "Score: " + score.ToString();
    }
}
