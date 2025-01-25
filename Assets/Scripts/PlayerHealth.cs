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

    // OLD: HealthBar object declaration
    // This method requires direct access to an component from another GameObject
    // It generates coupling between classes
    //public HealthBar HealthBar;   

    // NEW: Using Pattern Observer = events system

    // OPTION 1: Declare a delegate method and define an event object
    // public delegate void ChangeHealthDelegate(float fractionHealth);
    // public static event ChangeHealthDelegate OnChangeHealth;

    // OPTION 2: Declare an Action object (already an event)
    public static Action<float> OnChangeHealth;

    private void Start()
    {
        Health = MaxHealth;

        // OLD: Init HealthBar opject, picking it from scene
        //HealthBar = GameObject.Find("Slider").GetComponent<HealthBar>();
        //HealthBar.SetValue(Health / MaxHealth);

        // NEW: Call method subscribed to our new event
        OnChangeHealth?.Invoke(Health / MaxHealth);     // NOTE: '?' is syntactic sugar for handling null check
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Spike")
        {
            Health -= 10;
            //SendMessage("OnCoinCollected", this);
            OnChangeHealth?.Invoke(Health / MaxHealth);
            if (Health <= 0)
            {
                SceneManager.LoadScene("Ending");
            }
        }
    }
}
