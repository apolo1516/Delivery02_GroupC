using System;
using UnityEngine;

public class WinObserver : MonoBehaviour
{
    public static Action<WinObserver> OnWinScreen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            OnWinScreen?.Invoke(this);
        }
    }
}