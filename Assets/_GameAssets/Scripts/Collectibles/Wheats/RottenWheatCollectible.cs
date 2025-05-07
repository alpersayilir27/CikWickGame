using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float movementDecreaseSpeed;
    [SerializeField] private float resetBoostDuration;

    public void Collect()
    {
        playerController.SetMovementSpeed(movementDecreaseSpeed,resetBoostDuration);
        Destroy(gameObject);
    }
}
