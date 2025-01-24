using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public static Action<PowerUp> OnPowerUpCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // SendMessage("OnPowerUpCollected", this);
            OnPowerUpCollected?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
