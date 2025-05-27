using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int currentHealth = 100;
    public int maxHealth = 100;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
}

