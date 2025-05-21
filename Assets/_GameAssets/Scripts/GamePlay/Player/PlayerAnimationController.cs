using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    private PlayerController playerController;
    private StateController stateController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        stateController = GetComponent<StateController>();
    }

    private void Start()
    {
        playerController.OnPlayerJumped += PlayerController_OnPlayerJumped;
    }

    private void Update()
    {
        if (GameManager.Instance.GetCurentGameState() != GameState.Play && GameManager.Instance.GetCurentGameState() != GameState.Resume)
        {
            return;
        }
        SetPlayerAnimations();
    }

    private void PlayerController_OnPlayerJumped()
    {
        playerAnimator.SetBool(Const.PlayerAnimations.IS_JUMPING, true);
        Invoke(nameof(ResetJumping), 0.5f);
    }

    private void ResetJumping()
    {
        playerAnimator.SetBool(Const.PlayerAnimations.IS_JUMPING, false);

    }

    private void SetPlayerAnimations()
    {
        var currentState = stateController.GetCurrentState();

        switch(currentState)
        {
            case PlayerState.Idle:
                playerAnimator.SetBool(Const.PlayerAnimations.IS_SlIDING, false);
                playerAnimator.SetBool(Const.PlayerAnimations.IS_MOVING, false);
                break;
            case PlayerState.Move:
                playerAnimator.SetBool(Const.PlayerAnimations.IS_SlIDING, false);
                playerAnimator.SetBool(Const.PlayerAnimations.IS_MOVING, true);
                break; 
            case PlayerState.SlideIdle:
                playerAnimator.SetBool(Const.PlayerAnimations.IS_SlIDING, true);
                playerAnimator.SetBool(Const.PlayerAnimations.IS_SLIDING_ACTIVE, false);
                break;  
            case PlayerState.Slide:
                playerAnimator.SetBool(Const.PlayerAnimations.IS_SlIDING, true);
                playerAnimator.SetBool(Const.PlayerAnimations.IS_SLIDING_ACTIVE, true);
                break;
        }
    }

}
