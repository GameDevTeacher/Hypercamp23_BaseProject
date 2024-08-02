using UnityEngine;

namespace Steps
{
    public class Step4Movement : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float jumpSpeed = 7f;

        public float fallMultiplier = 4f;
        public float lowJumpMultiplier = 3f;
        private float _gravityY;
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
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, whatIsGround);

            if (_rigidbody2D.linearVelocity.y < 0)
            {
                _rigidbody2D.linearVelocity += Vector2.up * (_gravityY * (fallMultiplier - 1) * Time.deltaTime);
            }
            else if (_rigidbody2D.linearVelocity.y > 0 && !_input.Jumping)
            {
                _rigidbody2D.linearVelocity += Vector2.up * (_gravityY * (lowJumpMultiplier - 1) * Time.deltaTime);
            }

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