using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float ForceIncrease;
    [SerializeField] private float resetBoostDuration;

    public void Collect()
    {
        playerController.SetJumpForce(ForceIncrease,resetBoostDuration);
        Destroy(gameObject);
    }
}
