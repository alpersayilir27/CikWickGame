using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            //UI animate damage

            if (currentHealth <= 0)
            {
                //Player dies
            }
        }
    }

    public void Heal(int healAmount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        }
    }
    
}
