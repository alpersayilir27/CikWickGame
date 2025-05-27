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
        if (GameManager.Instance.GetCurentGameState() != GameState.Play && GameManager.Instance.GetCurentGameState() != GameState.Resume
        && GameManager.Instance.GetCurentGameState() != GameState.CutScene && GameManager.Instance.GetCurentGameState() != GameState.GameOver)
        {
            catAnimator.enabled = false;
            
            return;
        }
            
            
            
       
        SetCatAnimations();
    }

    private void SetCatAnimations()
    {
        catAnimator.enabled = true;
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
