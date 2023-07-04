using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private float _landAnimDuration = 0.1f;
    
    private InputManager _input;
    private Animator _anim;
    private SpriteRenderer _renderer;
    
    private bool _grounded;
    private float _lockedTill;
    private bool _jumpTriggered;
    private bool _landed;

    private void Awake() {
        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _input = GetComponent<InputManager>();
    }


    private void Update() {
        if (_input.Move.x != 0) _renderer.flipX = _input.Move.x < 0;

        var state = GetState();

        _jumpTriggered = false;
        _landed = false;

        if (state == _currentState) return;
        _anim.CrossFade(state, 0, 0);
        _currentState = state;
    }

    private int GetState() {
        if (Time.time < _lockedTill) return _currentState;

        // Priorities
      // if (_attacked) return LockState(Attack, _attackAnimTime);
       // if (_player.Crouching) return Crouch;
        if (_landed) return LockState(Land, _landAnimDuration);
        if (_jumpTriggered) return Jump;

        if (_grounded) return _input.Move.x == 0 ? Idle : Walk;
        return _input.Move.y > 0 ? Jump : Fall;

        int LockState(int s, float t) {
            _lockedTill = Time.time + t;
            return s;
        }
    }

    #region Cached Properties

    private int _currentState;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Fall = Animator.StringToHash("Fall");
    private static readonly int Land = Animator.StringToHash("Land");
   
    #endregion
}