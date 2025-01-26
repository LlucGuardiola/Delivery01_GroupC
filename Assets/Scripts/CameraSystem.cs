using System;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    GameObject cam;
    GameObject newDeathZone;
    GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float distanceToMove;
    private float newPosition;
    private bool moveCamera = false;

    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        newDeathZone = GameObject.Find("SecondDeathzone");
        player = GameObject.Find("Player");
        newPosition = cam.transform.position.y + distanceToMove;
    }

    private void OnEnable()
    {
        CameraObserver.OnCameraMovement += MoveCamera;
    }

    private void OnDisable()
    {
        CameraObserver.OnCameraMovement -= MoveCamera;
    }

    private void Update()
    {
        if (moveCamera)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, 
                                                 cam.transform.position.y + speed * Time.deltaTime, 
                                                 cam.transform.position.z);
        }
        if (moveCamera && cam.transform.position.y > newPosition) 
        {
            cam.transform.position = new Vector3(cam.transform.position.x, newPosition, cam.transform.position.z);
            moveCamera = false;
            Destroy(gameObject);
        }
    }
    private void MoveCamera(CameraObserver cam)
    {
        moveCamera = true;
        newDeathZone.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<PlayerMove>().spawnpoint = player.transform.position;
        
    }
}
