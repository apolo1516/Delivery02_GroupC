using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public int Value = 5;
    public static Action<Coin> OnCoinCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            OnCoinCollected?.Invoke(this);
            Destroy(gameObject);
        }
    }
}