using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Hypercamp_23 _inputAction;

    public Vector2 Move { get; private set; }
    public bool Jump { get; private set; }
    public bool Jumping { get; private set; }
    public bool Jumped { get; private set; }
    public bool Interact { get; private set; }
    public bool Sprint { get; private set; }

    private void Awake()
    {
        _inputAction = new Hypercamp_23();
    }

    private void Update()
    {
        Move = _inputAction.Player.Move.ReadValue<Vector2>();
        Jump = _inputAction.Player.Jump.triggered;
        Jumping = _inputAction.Player.Jump.inProgress;
        Jumped = _inputAction.Player.Jump.WasReleasedThisFrame();
        Interact = _inputAction.Player.Interact.triggered;
        Sprint = _inputAction.Player.Sprint.inProgress;
    }
    
    private void OnEnable() => _inputAction.Enable();
    private void OnDisable() => _inputAction.Disable();
}