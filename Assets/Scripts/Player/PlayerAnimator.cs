using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake() { _animator = GetComponent<Animator>(); }
    
    public void PlayAnimation(string animationName)
    {
        _animator.Play(animationName);
    }
}
