using UnityEngine;
using UnityEngine.InputSystem;

namespace Steps
{
    public class Step3Movement : MonoBehaviour
    {
        public float speed = 3f;
        public Vector2 newVelocity;
        public bool isGrounded;

        public LayerMask whatIsGround;

        private InputManager _input;
        private Rigidbody2D _rigidbody2D;


        private void Start()
        {
         
            _input = GetComponent<InputManager>();
           
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.gravityScale = 1f;
        }

        private void Update()
        {
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, whatIsGround);
        }
        
        
        private void FixedUpdate()
        {
            // Jump
            
        
            // Movement

            newVelocity = new Vector2(_input.Move.x * speed, _rigidbody2D.velocity.y);
            _rigidbody2D.velocity = newVelocity;
        }
    }
}