using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{

    [SerializeField] private Transform playerVisualTransform;
    private PlayerController playerController;
    private Rigidbody playerRigidbody;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerRigidbody = GetComponent<Rigidbody>();
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.Collect();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IBoostable>(out var boostable))
        {
            boostable.Boost(playerController);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<IDamageble>(out var damageble))
        {
            damageble.GiveDamage(playerRigidbody, playerVisualTransform);
            CameraShake.Instance.ShakeCamera(1f, 0.5f);
        }
    }
        
}
