using UnityEngine;

public class CatStateController : MonoBehaviour
{
    [SerializeField] private CatState currentCatState = CatState.Idle;

    private void Start()
    {
        ChangeState(CatState.Walking);
    }

    public void ChangeState(CatState newState)
    {
        if (currentCatState == newState) { return; }

        currentCatState = newState;


    }

    public CatState GetCurrentState()
    {
        return currentCatState;
    }
}
