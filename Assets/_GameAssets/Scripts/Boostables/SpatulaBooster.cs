using UnityEngine;

public class SpatulaBooster : MonoBehaviour, IBoostable
{
    [Header("References")]
    [SerializeField] private Animator spatulaAnimator;

    [Header("Settings")]
    [SerializeField] private float jumpForce;

    private bool isActivated;
    public void Boost(PlayerController playerController)
    {
        if (isActivated) return;
        PlayBoostAnimation();
        Rigidbody playerRigidbody = playerController.GetPlayerRigidbody();

        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
        playerRigidbody.AddForce(transform.forward * jumpForce, ForceMode.Impulse);
        isActivated = true;
        Invoke(nameof(ResetActivation), 0.2f);
    }

    private void PlayBoostAnimation()
    {
        spatulaAnimator.SetTrigger(Const.OtherAnimations.IS_SPATULA_JUMPING);
    }

    private void ResetActivation()
    {
        isActivated = false;
    }

}

