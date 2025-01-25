using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSystem : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
       player = GameObject.Find("Player");
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
        // Jump PowerUp
        player.GetComponent<PlayerJumper>().JumpHeight += 1;
        player.GetComponent<PlayerJumper>().SpeedHorizontal -= 1;
    }
}
