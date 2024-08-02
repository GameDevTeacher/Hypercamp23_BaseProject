using UnityEngine;

namespace Steps
{
    public class Step3Movement : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float jumpSpeed = 7f;
        
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
            // Are we on ground?
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, whatIsGround);
            if (!isGrounded) return;

            // Jump
            if (_input.Jump)
            {
                _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, jumpSpeed);
            }
        }

        private void FixedUpdate()
        {
            // Movement
            _rigidbody2D.linearVelocity = new Vector2(_input.Move .x * moveSpeed, _rigidbody2D.linearVelocity.y);
        }
    }
}