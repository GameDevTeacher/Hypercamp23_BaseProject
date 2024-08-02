using UnityEngine;


namespace Steps
{
    public class Step7Movement : MonoBehaviour
    {
        [Header("Core Movement")] [Space(5)]
        public float moveSpeed = 5f;
        public float jumpSpeed = 7f;
        public Vector2 newVelocity;
        
        [Header("Ground Check")]
        public bool isGrounded;
        public LayerMask whatIsGround;
        
        [Header("BetterJump")] [Space(5)] 
        public float coyoteTime = 0.2f;
        public float coyoteTimeCounter;

        public float jumpBufferTime = 0.2f;
        public float jumpBufferCounter;
        
        [Header("Acceleration")] [Space(5)]
        public float acceleration = 0.02f;
        public float groundFriction = 0.03f;
        public float airFriction = 0.005f;

        
        [Header("Components")] [Space(5)]
        private InputManager _input;
        private Rigidbody2D _rigidbody2D;
        
       
        private void Start()
        {
            _input = GetComponent<InputManager>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // Are we on ground?
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.55f, whatIsGround);

            // CoyoteTime
            if (isGrounded)
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= 1 * Time.deltaTime;
            }

            // Jump Buffer
            if (_input.Jump)
            {
                jumpBufferCounter = jumpBufferTime;
            }
            else
            {
                jumpBufferCounter -= 1 * Time.deltaTime;
            }

            // Better Jump
            if (coyoteTimeCounter > 0 && jumpBufferCounter > 0)
            {
                _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, jumpSpeed);
                jumpBufferCounter = 0f;
            }
            if (_input.Jumped && _rigidbody2D.linearVelocity.y > 0f)
            {
                _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, _rigidbody2D.linearVelocity.y * 0.5f);
                coyoteTimeCounter = 0f;
            }
        }

        private void FixedUpdate()
        {
            // Acceleration & Friction
            if (_input.Move.x != 0)
            {
                newVelocity.x = Mathf.Lerp(newVelocity.x, moveSpeed * _input.Move.x, acceleration);
            }
            else
            {
                newVelocity.x = Mathf.Lerp(newVelocity.x, 0f, isGrounded ? groundFriction : airFriction);
            }

            // Movement
            _rigidbody2D.linearVelocity = new Vector2(newVelocity.x, _rigidbody2D.linearVelocity.y);
        }
    }
}