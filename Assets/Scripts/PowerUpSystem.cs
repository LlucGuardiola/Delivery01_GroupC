using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PowerUpSystem : MonoBehaviour
{
    GameObject player = GameObject.Find("Player");

    private void OnEnable()
    {
        Debug.Log("skiper");
        PowerUp.OnPowerUpCollected += UpdateJump;
    }

    private void OnDisable()
    {
        Debug.Log("skiper3");
        PowerUp.OnPowerUpCollected -= UpdateJump;
    }
    private void UpdateJump(PowerUp powerUp)
    {
        Debug.Log("skiper2");
        player.GetComponent<PlayerJumper>().SpeedHorizontal += 2;
    }
}
