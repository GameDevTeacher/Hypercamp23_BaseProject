using UnityEngine;

// TODO: Acceleration v
// TODO: Coyote Time
// TODO: Jump Buffer
// TODO: Variable Jump Height Retool
namespace Steps
{
    public class Step5Movement : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float jumpSpeed = 7f;

        public float fallMultiplier = 4f;
        public float lowJumpMultiplier = 3f;
        private float _gravityY;
        
        public Vector2 newVelocity;

        public float acceleration = 0.3f;
        public float deceleration = 0.4f;
        public float airDeceleration = 0.1f;
        public bool isGrounded;

        public LayerMask whatIsGround;

        private InputManager _input;
        private Rigidbody2D _rigidbody2D;
        
       
        private void Start()
        {
            _gravityY = Physics2D.gravity.y;
            _input = GetComponent<InputManager>();
           
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.gravityScale = 1f;
        }

        private void Update()
        {
            Attack();
            // Are we on ground?
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, whatIsGround);

            if (_rigidbody2D.linearVelocity.y < 0)
            {
                _rigidbody2D.linearVelocity += Vector2.up * (_gravityY * (fallMultiplier - 1) * Time.deltaTime);
            }
            else if (_rigidbody2D.linearVelocity.y > 0 && !_input.Jumping)
            {
                _rigidbody2D.linearVelocity += Vector2.up * (_gravityY * (lowJumpMultiplier - 1) * Time.deltaTime);
            }
            
            /*// Acceleration Check
            if (_input.Move.x != 0)
            {
                newVelocity.x = Mathf.Lerp(newVelocity.x, moveSpeed * _input.Move.x, acceleration);
            }
            else
            {
                if (newVelocity.x is < 0.1f and > -0.1f)
                {
                    newVelocity.x = 0;
                }
                else
                {
                    newVelocity.x = Mathf.Lerp(newVelocity.x, 0, deceleration);
                }
            }*/
            
            

            if (!isGrounded) return;
            
            // Jump
            if (_input.Jump)
            {
                _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, jumpSpeed);
            }
        }

        private void FixedUpdate()
        {
            if (_input.Move.x != 0)
            {
                newVelocity.x = Mathf.Lerp(newVelocity.x, moveSpeed * _input.Move.x, acceleration);
            }
            else
            {
                newVelocity.x = Mathf.Lerp(newVelocity.x, 0f, isGrounded ? deceleration : airDeceleration);
            }
            // Only used if we have air control && _collision.IsGroundedBox()
            /*if (_input.Move.x != 0  )
            {
                newVelocity.x += _input.Move.x * acceleration;
                newVelocity.x = Mathf.Clamp(moveSpeed, -maxVelocityX, maxVelocityX);
            }
            else
            {
                newVelocity.x = Mathf.Lerp(_horizontalMoveSpeed, 0, _collision.IsGroundedBox() ? groundFriction : airFriction);
            }
            */
            
            // Movement
            _rigidbody2D.linearVelocity = new Vector2(newVelocity.x, _rigidbody2D.linearVelocity.y);
        }

        private void Attack()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);
            if (hit.collider == null) return;

            if (!hit.transform.CompareTag("Enemy")) return;
            
            Destroy(hit.transform.gameObject);
            _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, jumpSpeed);
        }
    }
}