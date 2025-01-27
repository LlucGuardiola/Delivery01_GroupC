using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PowerUpSystem : MonoBehaviour
{
    GameObject player;
    public AudioClip PowerUpSound;
    private AudioSource audioSource;
    private void Start()
    {
       player = GameObject.Find("Player");
       audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        PowerUp.OnPowerUpCollected += UpdateJump;
    }

    private void OnDisable()
    {
        PowerUp.OnPowerUpCollected -= UpdateJump;
    }
    private void UpdateJump(PowerUp powerUp)
    {
        if (audioSource != null && PowerUpSound != null)
        {
            audioSource.PlayOneShot(PowerUpSound);
        }

        player.GetComponent<PlayerJumper>().JumpHeight += 1;
        player.GetComponent<PlayerJumper>().SpeedHorizontal -= 1;
    }
}
