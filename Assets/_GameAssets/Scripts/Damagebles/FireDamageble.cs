using UnityEngine;

public class FireDamageble : MonoBehaviour, IDamageble
{
    [SerializeField] private float force = 10f;

    public void GiveDamage(Rigidbody playerRigidbody, Transform playerVisualTransform)
    {
        HealthManager.Instance.Damage(1);
        playerRigidbody.AddForce(-playerVisualTransform.forward * force, ForceMode.Impulse);
        Destroy(gameObject);

    }
}
