using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public int Score;

    //public delegate void OnScoredDelegate(int score);
    //public static event OnScoredDelegate OnScored;

    public static Action<int> OnScoreUpdated;

    private void OnEnable()
    {
        Debug.Log("hola");
        Coin.OnCoinCollected += UpdateScore;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= UpdateScore;
    }

    private void UpdateScore(Coin coin)
    {
        Score += coin.Value;

        OnScoreUpdated?.Invoke(Score);
    }

    void Start() 
    { 
    
    }

    void Update()
    {
        
    }
}
