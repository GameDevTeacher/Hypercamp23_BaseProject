using UnityEngine;
using UnityEngine.InputSystem;

namespace Steps
{
    public class Step1Movement : MonoBehaviour
    {
        public float speed = 3f;

        public Vector2 MoveVector;
        public bool isJumping;
        
        private InputManager _input;
        private Rigidbody2D _rigidbody2D;
        
        private Animator _animator;
        

        private void Start()
        {
            _rigidbody2D.gravityScale = 0f;
            _input = GetComponent<InputManager>();
           
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            
            
           
        }

        private void Update()
        {
            GetInput();
        }
        
        
        
        private void FixedUpdate()
        {
            if (MoveVector.magnitude > 1)
            {
                MoveVector = MoveVector.normalized;
            }
            _rigidbody2D.velocity = MoveVector * speed;
        }

        private void GetInput()
        {
            MoveVector.x = (Keyboard.current.dKey.isPressed ? 1 : 0) + (Keyboard.current.aKey.isPressed ? -1 : 0);
            MoveVector.y = (Keyboard.current.wKey.isPressed ? 1 : 0) + (Keyboard.current.sKey.isPressed ? -1 : 0);
            isJumping = Keyboard.current.spaceKey.wasPressedThisFrame;
        }
    }
}