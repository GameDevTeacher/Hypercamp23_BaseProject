using UnityEngine;
using UnityEngine.InputSystem;

namespace Steps
{
    public class Step2Movement : MonoBehaviour
    {
        public float speed = 3f;

        public Vector2 moveVector;
        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.gravityScale = 0f;
        }

        private void Update()
        {
            GetInput();
        }
        
        
        
        private void FixedUpdate()
        {
            if (moveVector.magnitude > 1)
            {
                moveVector = moveVector.normalized;
            }
            _rigidbody2D.linearVelocity = moveVector * speed;
        }

        private void GetInput()
        {
            moveVector.x = (Keyboard.current.dKey.isPressed ? 1 : 0) + (Keyboard.current.aKey.isPressed ? -1 : 0);
            moveVector.y = (Keyboard.current.wKey.isPressed ? 1 : 0) + (Keyboard.current.sKey.isPressed ? -1 : 0);
        }
    }
}