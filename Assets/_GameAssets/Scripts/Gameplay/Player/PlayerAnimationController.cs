using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _PlayerAnimator;

    private PlayerControl _PlayerControl;
    private StateController _StateController;

    private void Awake()
    {
        _PlayerControl = GetComponent<PlayerControl>();
        _StateController = GetComponent<StateController>();
    }

    private void Start()
    {
        _PlayerControl.OnPlayerJumped += PlayerConroller_OnPlayerJumped;
    }

    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play
            && GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        {
            return;
        }

        SetPlayerAnimations();
    }
    private void PlayerConroller_OnPlayerJumped()
    {
        _PlayerAnimator.SetBool(Consts.PlayerAnimation.IS_JUMPING, true);
        Invoke(nameof(ResetJumping), 0.5f);
    }

    private void ResetJumping()
    {
        _PlayerAnimator.SetBool(Consts.PlayerAnimation.IS_JUMPING, false);
    }

    private void SetPlayerAnimations()
    {
        var currentState = _StateController.GetCurrentState();

        switch (currentState)
        {
            case PlayerState.Idle:
                _PlayerAnimator.SetBool(Consts.PlayerAnimation.IS_SLIDING, false);
                _PlayerAnimator.SetBool(Consts.PlayerAnimation.IS_MOVING, false);
                break;

            case PlayerState.Move:
                _PlayerAnimator.SetBool(Consts.PlayerAnimation.IS_SLIDING, false);
                _PlayerAnimator.SetBool(Consts.PlayerAnimation.IS_MOVING,true);
                break;

            case PlayerState.SlideIdle:
                _PlayerAnimator.SetBool(Consts.PlayerAnimation.IS_SLIDING, true);
                _PlayerAnimator.SetBool(Consts.PlayerAnimation.IS_SLIDING_ACTIVE, false);
                break;

            case PlayerState.Slide:
                _PlayerAnimator.SetBool(Consts.PlayerAnimation.IS_SLIDING, true);
                _PlayerAnimator.SetBool(Consts.PlayerAnimation.IS_SLIDING_ACTIVE, true);
                break;
        }
    }
}
