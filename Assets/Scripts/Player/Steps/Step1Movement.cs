using UnityEngine;
using UnityEngine.InputSystem;

namespace Steps
{
    public class Step1Movement : MonoBehaviour
    {
        public float speed = 3f;
        
        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.gravityScale = 0f;
        }

        private void Update()
        {
            if (Keyboard.current.aKey.isPressed)
            {
              transform.Translate(Vector2.left * (speed * Time.deltaTime));
            }
            else if (Keyboard.current.dKey.isPressed)
            {
                transform.Translate(Vector2.right * (speed * Time.deltaTime));
            }
            else if (Keyboard.current.wKey.isPressed)
            {
                transform.Translate(Vector2.up * (speed * Time.deltaTime));
            }
            else if (Keyboard.current.sKey.isPressed)
            {
                transform.Translate(Vector2.down * (speed * Time.deltaTime));
            }
        }
    }
}