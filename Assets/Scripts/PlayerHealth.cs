using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerHealth : MonoBehaviour
{
    [Range(0,100)]
    public float Health;
    public float MaxHealth = 100;

    public static Action<float> OnChangeHealth;

    private void Start()
    {
        Health = MaxHealth;
        OnChangeHealth?.Invoke(Health / MaxHealth);    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Spike")
        {
            Health -= 10;
            OnChangeHealth?.Invoke(Health / MaxHealth);
            if (Health <= 0)
            {
                SceneManager.LoadScene("Ending");
            }
            GetComponent<Animator>().SetTrigger("takeDamage");
            GetComponent<Transform>().position = GetComponent<PlayerMove>().spawnpoint;
        }
    }
}
