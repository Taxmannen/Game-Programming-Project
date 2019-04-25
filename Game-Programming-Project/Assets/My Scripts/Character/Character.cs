using UnityEngine;

public class Character : MonoBehaviour
{
    protected int maxHealth;
    protected int currentHealth;

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void RemoveCurrentHealth(int damage)
    {
        currentHealth -= Mathf.Abs(damage);
        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        Debug.Log("Dead");
    }
}