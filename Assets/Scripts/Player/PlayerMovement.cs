using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

	[Header("Core Movement")] [Space(5)] 
	public const float MoveSpeed = 5f;
	public const float JumpSpeed = 7f;
	public Vector2 newVelocity;

	[Header("Health")] 
	public int health = 3;
	public const int MaxHealth = 3;
	public bool canTakeDamage = true;
	public const float CanTakeDamageTime = 0.2f;

	[Header("Ground Check")] 
	public bool isGrounded;
	public LayerMask whatIsGround;

	[Header("BetterJump")] [Space(5)] 
	public const float CoyoteTime = 0.2f;
	public float coyoteTimeCounter;

	public const float JumpBufferTime = 0.2f;
	public float jumpBufferCounter;

	[Header("Acceleration")] [Space(5)] 
	public const float Acceleration = 0.02f;
	public const float GroundFriction = 0.03f;
	public const float AirFriction = 0.005f;


	[Header("Components")] [Space(5)] 
	private InputManager _input;
	private Rigidbody2D _rigidbody2D;
	private PlayerAnimator _animator;
	private SpriteRenderer _renderer;

	private void Start()
	{
		_input = GetComponent<InputManager>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_animator = GetComponent<PlayerAnimator>();
		_renderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		isGrounded = IsGrounded();
		UpdateAttack();
		//UpdateAnimation();
		UpdateJumping();
	}

	private void FixedUpdate()
	{
		UpdateMovement();
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		UpdateHealth(collision);
	}


	private void UpdateMovement()
	{
		// Acceleration & Friction
		if (_input.Move.x != 0)
		{
			newVelocity.x = Mathf.Lerp(newVelocity.x, MoveSpeed * _input.Move.x, Acceleration);
		}
		else
		{
			newVelocity.x = Mathf.Lerp(newVelocity.x, 0f, IsGrounded() ? GroundFriction : AirFriction);
		}

		// Movement
		_rigidbody2D.linearVelocity = new Vector2(newVelocity.x, _rigidbody2D.linearVelocity.y);
	}

	public bool IsGrounded()
	{
		return Physics2D.Raycast(transform.position, Vector2.down, 0.55f, whatIsGround);
	}

	private void UpdateAttack()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);
		if (hit.collider == null) return;

		if (!hit.transform.CompareTag("Enemy")) return;

		Destroy(hit.transform.gameObject);
		_rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, JumpSpeed);
	}

	private void UpdateHealth(Collider2D collision)
	{
		if (health == 0)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		
		if (collision.CompareTag("Enemy") && canTakeDamage)
		{
			health -= 1;
			StartCoroutine(DamageInvincibility());
			canTakeDamage = false;
		}

		if (collision.CompareTag("Heart"))
		{
			health += 1;
		}
	}

	private IEnumerator DamageInvincibility()
	{
		yield return new WaitForSeconds(CanTakeDamageTime);
		//canTakeDamage = true;
	}

	public void CoyoteJumpBuffer()
	{
	    if (IsGrounded())
	    {
	        coyoteTimeCounter = CoyoteTime;
	    }
	    else
	    {
	        coyoteTimeCounter -= 1 * Time.deltaTime;
	    }

	    // Jump Buffer
	    if (_input.Jump)
	    {
	        jumpBufferCounter = JumpBufferTime;
	    }
	    else
	    {
	        jumpBufferCounter -= 1 * Time.deltaTime;
	    }
	}

	public void UpdateJumping()
	{
		CoyoteJumpBuffer();
	    if (coyoteTimeCounter > 0 && jumpBufferCounter > 0)
	    {
	        _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, JumpSpeed);
	        jumpBufferCounter = 0f;
	    }
	    if (_input.Jumped && _rigidbody2D.linearVelocity.y > 0f)
	    {
	        _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, _rigidbody2D.linearVelocity.y * 0.5f);
	        coyoteTimeCounter = 0f;
	    }
	}

	private void UpdateAnimation()
	{
		if (_input.Move.x != 0)
		{
			_renderer.flipX = _input.Move.x < 0;
		}
		
        if (IsGrounded())
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
}