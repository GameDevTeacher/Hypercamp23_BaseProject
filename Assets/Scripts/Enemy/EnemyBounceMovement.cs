using System;
using UnityEngine;

public class EnemyBounceMovement : MonoBehaviour
{
    public float moveSpeed = 4f;

    private Rigidbody2D _rigidbody2D;
    public GameObject ForwardPoint;
    public LayerMask whatIsGround;

    public bool flip;

    private void Start()
    {
        flip = true;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(transform.localScale.x * moveSpeed, 0f);
    }

    private void LateUpdate()
    {
        if (DetectWallOrFall())
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
            flip = false;
        }
    }

    private bool DetectWallOrFall()
    {
        /*return !Physics2D.Raycast(ForwardPoint.transform.position, Vector2.down, 0.6f, whatIsGround) 
               || (Physics2D.Raycast(transform.position, Vector2.right , 0.6f, whatIsGround) 
                   ||Physics2D.Raycast(transform.position, Vector2.left , 0.6f, whatIsGround)
               );*/
        return !RayCaster(ForwardPoint.transform.position, Vector2.down) 
               || (RayCaster(Vector2.right) || RayCaster(Vector2.left));
    }

    private bool RayCaster(Vector3 position, Vector2 direction)
    {
        return Physics2D.Raycast(position, direction, 0.6f, whatIsGround);
    }
    private bool RayCaster(Vector2 direction)
    {
        return Physics2D.Raycast(transform.position, direction, 0.6f, whatIsGround);
    }
}
