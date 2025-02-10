using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    public float JumpHeight;
    public float FallSpeedMultiplier;
    public float DistanceToMaxHeight;
    public float SpeedHorizontal;
    public float PressTimeToMaxJump;
    public float WallSlideSpeed = 1;
    public float MaxFallSpeed;
    public int NumberOfJumps;
    public ContactFilter2D filter;
    public AudioClip JumpSound; 

    private float _lastVelocityY;
    private float _jumpStartedTime;
    private int currentJumps = 0;
    private Rigidbody2D _rigidbody;
    private CollisionDetection _collisionDetection;
    private AudioSource audioSource;


    bool IsWallSliding => _collisionDetection.IsTouchingFront;
    bool IsTouchingGround => _collisionDetection.IsGrounded;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionDetection = GetComponent<CollisionDetection>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (IsPeakReached()) TweakGravity();

        if (IsWallSliding) SetWallSlide();

        if (IsTouchingGround) currentJumps = 1; 
    }

    private void Update()
    {
        GetComponent<Animator>().SetBool("isOnAir?", !IsTouchingGround);
    }

    public void OnJumpStarted()
    {
        if (NumberOfJumps <= currentJumps) return;

        GetComponent<Animator>().SetTrigger("startJump");
        SetGravity();
        var vel = new Vector2(_rigidbody.linearVelocity.x, GetJumpForce());
        _rigidbody.linearVelocity = vel;
        _jumpStartedTime = Time.time;
        currentJumps += 1;
        if (audioSource != null && JumpSound != null)
        {
            audioSource.PlayOneShot(JumpSound);
        }
    }

    public void OnJumpFinished()
    {
        float fractionOfTimePressed = 1 / Mathf.Clamp01((Time.time - _jumpStartedTime) / PressTimeToMaxJump);
        _rigidbody.gravityScale *= fractionOfTimePressed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        float h = -GetDistanceToGround() + JumpHeight;
        Vector3 start = transform.position + new Vector3(-1, h, 0);
        Vector3 end = transform.position + new Vector3(1, h, 0);
        Gizmos.DrawLine(start, end);
        Gizmos.color = Color.white;
    }
    
    private bool IsPeakReached()
    {
        bool reached = (_lastVelocityY * _rigidbody.linearVelocity.y) < 0;
        _lastVelocityY = _rigidbody.linearVelocity.y;

        return reached;
    }

    private void SetWallSlide()
    {
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 
            Mathf.Max(_rigidbody.linearVelocity.y, -WallSlideSpeed));
    }

    private void SetGravity()
    {
        var grav = 2 * JumpHeight * (SpeedHorizontal * SpeedHorizontal) / (DistanceToMaxHeight * DistanceToMaxHeight);
        _rigidbody.gravityScale = grav / 9.81f;
    }

    private void TweakGravity()
    {
        _rigidbody.gravityScale *= FallSpeedMultiplier;
        _rigidbody.gravityScale = Mathf.Clamp(_rigidbody.gravityScale, -Mathf.Infinity, MaxFallSpeed); 
    }

    private float GetJumpForce()
    {
        return 2 * JumpHeight * SpeedHorizontal / DistanceToMaxHeight;
    }

    private float GetDistanceToGround()
    {
        RaycastHit2D[] hit = new RaycastHit2D[3];

        Physics2D.Raycast(transform.position, Vector2.down, filter, hit, 10);

        return hit[0].distance;
    }
}
