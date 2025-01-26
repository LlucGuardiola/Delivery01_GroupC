using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5.0f;

    public Vector3 spawnpoint { get; private set; }

    Rigidbody2D _rigidbody;
    private float _horizontalDir;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); 
        spawnpoint = GetComponent<Transform>().position;
    }

    void FixedUpdate()
    {
        Vector2 velocity = _rigidbody.linearVelocity;
        velocity.x = _horizontalDir * Speed;
        _rigidbody.linearVelocity = velocity;
    }

    // NOTE: InputSystem: "move" action becomes "OnMove" method
    void OnMove(InputValue value)
    {
        // Read value from control, the type depends on what
        // type of controls the action is bound to
        var inputVal = value.Get<Vector2>();
        _horizontalDir = inputVal.x;
    }
    private void Update()
    {
        if(_horizontalDir != 0) { GetComponent<Animator>().SetBool("canRun?",true);}
        else { GetComponent<Animator>().SetBool("canRun?", false);}

        if (_horizontalDir < 0) { GetComponent<SpriteRenderer>().flipX = true;}
        if (_horizontalDir > 0) { GetComponent<SpriteRenderer>().flipX = false;}
    }
}
