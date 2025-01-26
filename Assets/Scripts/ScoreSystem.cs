using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreSystem : MonoBehaviour
{
    public int Score;

    public static Action<int> OnScoreUpdated;

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
        OnScoreUpdated?.Invoke(Score);
    }
}
