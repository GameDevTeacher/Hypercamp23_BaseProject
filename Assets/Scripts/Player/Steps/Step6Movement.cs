using UnityEngine;

// TODO: Coyote Time
// TODO: Jump Buffer
namespace Steps
{
    public class Step6Movement : MonoBehaviour
    {
        [Header("Core Movement")] [Space(5)]
        public float moveSpeed = 5f;
        public float jumpSpeed = 7f;
        public Vector2 newVelocity;
        public bool isGrounded;
        
        [Header("Acceleration")] [Space(5)]
        public float acceleration = 0.3f;
        public float groundFriction = 0.4f;
         public float airFriction = 0.1f;

        public LayerMask whatIsGround;
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
            Attack();
            // Are we on ground?
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, whatIsGround);

            // Better Jump
            if (isGrounded && _input.Jump)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpSpeed);
            }

            if (_input.Jumped && _rigidbody2D.velocity.y > 0f)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 0.5f);
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
            _rigidbody2D.velocity = new Vector2(newVelocity.x, _rigidbody2D.velocity.y);
        }

        private void Attack()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);
            if (hit.collider == null) return;

            if (!hit.transform.CompareTag("Enemy")) return;
            
            Destroy(hit.transform.gameObject);
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpSpeed);
        }
    }
}