using UnityEngine;


public class InputManager : MonoBehaviour
{
    private Hypercamp_23 _inputAction;
    
    public Vector2 Move { get; private set; }
    public bool Jump { get; private set; }
    public bool Interact { get; private set; }

    private void Start()
    {
        _inputAction = new Hypercamp_23();
    }

    private void Update()
    {
        Move = _inputAction.Player.Move.ReadValue<Vector2>();
        Jump = _inputAction.Player.Jump.triggered;
        Interact = _inputAction.Player.Interact.triggered;
    }
    
    private void OnEnable() => _inputAction.Enable();

    private void OnDisable() => _inputAction.Disable();
}
