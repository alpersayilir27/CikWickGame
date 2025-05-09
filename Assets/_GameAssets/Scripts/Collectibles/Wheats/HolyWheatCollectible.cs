using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerController playerController;
    
    [SerializeField] private WheatDesignSO wheatDesignSO; 

    public void Collect()
    {
        playerController.SetJumpForce(wheatDesignSO.IncreaseDecreaseMultiplier, wheatDesignSO.ResetBoostDuration);
        Destroy(gameObject);
    }
}
