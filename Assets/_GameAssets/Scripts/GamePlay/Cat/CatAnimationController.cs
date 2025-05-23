using UnityEngine;

public class CatAnimationController : MonoBehaviour
{
    [SerializeField] private Animator catAnimator;

    private CatStateController catStateController;

    void Awake()
    {
        catStateController = GetComponent<CatStateController>();
    }

    void Update()
    {
        SetCatAnimations();
    }

    private void SetCatAnimations()
    {
        var currentCatState = catStateController.GetCurrentState();

        switch (currentCatState)
        {
            case CatState.Idle:
                catAnimator.SetBool(Const.CatAnimations.IS_IDLING, true);
                catAnimator.SetBool(Const.CatAnimations.IS_WALKING, false);
                catAnimator.SetBool(Const.CatAnimations.IS_RUNNING, false);
                break;
            case CatState.Walking:
                catAnimator.SetBool(Const.CatAnimations.IS_IDLING, false);
                catAnimator.SetBool(Const.CatAnimations.IS_WALKING, true);
                catAnimator.SetBool(Const.CatAnimations.IS_RUNNING, false);
                break;
            case CatState.Running:
                catAnimator.SetBool(Const.CatAnimations.IS_RUNNING, true);
                break;
            case CatState.Attacking:
                catAnimator.SetBool(Const.CatAnimations.IS_ATTACKING, true);
                break;
            
        }
    }
}
