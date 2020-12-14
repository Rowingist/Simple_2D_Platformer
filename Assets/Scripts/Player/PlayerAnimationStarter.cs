using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationStarter : PlayerMover
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Update()
    {
        AnimateRun(PlayerHorisontalImput.Player.Move.ReadValue<Vector2>());

        if (PlayerVerticalImput.Player.Jump.triggered && Grounded)
            AnimateJump();
    }

    private void AnimateRun(Vector2 velocity)
    {
        _animator.SetBool("IsRunning", true);
        if (velocity.x > 0.1f)
        {
            _spriteRenderer.flipX = true;
        }
        else if (velocity.x < -0.1f)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }
    }

    private void AnimateJump()
    {
        _animator.SetBool("IsRunning", false);
        _animator.SetTrigger("Jump");
    }
}
