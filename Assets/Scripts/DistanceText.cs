using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DistanceText : MonoBehaviour
{
    private Text _label;

    private void Awake()
    {
        _label = GetComponent<Text>();
    }

    private void OnEnable()
    {
        // PlayerMovement.OnDistanceUpdated += UpdateDistanceText;
    }

    private void OnDisable()
    {
        // PlayerMovement.OnDistanceUpdated -= UpdateDistanceText;
    }

    private void UpdateDistanceText(int score)
    {
        _label.text = "SCORE: " + score.ToString();
    }
}
