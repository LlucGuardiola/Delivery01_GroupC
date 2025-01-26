using System;
using UnityEngine;

public class CameraObserver : MonoBehaviour
{
    public static Action<CameraObserver> OnCameraMovement;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            OnCameraMovement?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
