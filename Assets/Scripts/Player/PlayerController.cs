using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Speed;
    public Vector2 newVelocity;
    public bool isGrounded;

    public LayerMask whatIsGround;

    private PlayerAnimator _animator;
    private InputManager _input;
    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody2D;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<PlayerAnimator>();
        _input = GetComponent<InputManager>();
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    

    // Update is called once per frame
    void Update()
    {
        CheckGroundCollision();
        ApplyAnimations();
    }

    private void FixedUpdate()
    {
        
        // Jump
        
        
        
        // Movement
        newVelocity = new Vector2(_input.Move.x * Speed, _rigidbody2D.linearVelocity.y);
        _rigidbody2D.linearVelocity = newVelocity;
    }

    void CheckGroundCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, whatIsGround);
    }

    private void ApplyAnimations()
    {
        if (_input.Move.x != 0) _renderer.flipX = _input.Move.x < 0;

        if (isGrounded)
        {
            if (_input.Move.x == 0)
            {
                _animator.PlayAnimation("Idle");
            }
            else
            {

                if (_input.Move.x > 0)
                {
                    _animator.PlayAnimation("Walk_Right");
                }
                else
                {
                    _animator.PlayAnimation("Walk_Left");
                }
                
                
            }
        }
        else
        {
            if (_input.Move.y > 0)
            {
                _animator.PlayAnimation("Jump");
            }
            else
            {
                _animator.PlayAnimation("Fall");
            }
        }
    }

    private void ApplyAnimationsAdvanced()
    {
        if (_input.Move.x != 0) _renderer.flipX = _input.Move.x < 0;

        _animator.PlayAnimation(isGrounded ? _input.Move.x == 0 ? "Idle" 
            : _input.Move.x > 0 ? "Walk_Right" : "Walk_Left" 
            : _input.Move.y > 0 ? "Jump" : "Fall");
        
        //_animator.PlayAnimation(_input.Move.y > 0 ? "Jump" : "Fall");
        //_animator.PlayAnimation(_input.Move.x == 0 ? "Idle" : _input.Move.x > 0 ? "Walk_Right" : "Walk_Left");

        //_animator.PlayAnimation(_input.Move.x > 0 ? "Walk_Right" : "Walk_Left");
    }
}


