using UnityEngine;

/// <summary>
/// Example: a player health component that drives the HUD's HealthBarController.
/// Attach this to your Player GameObject and drag the HUD's HealthBarController
/// into the healthBar field in the Inspector.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBarController healthBar;
    [SerializeField] private int maxHealth = 10;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = maxHealth;

        // Sync the HUD to the starting value once, in case it defaults
        // to something else in the Inspector.
        healthBar.SetHealth(_currentHealth);
    }

    // Call this from wherever damage happens — an enemy attack, a trap,
    // a projectile hit, OnCollisionEnter, etc.
    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        healthBar.Damage(amount); // updates the hearts directly

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    // Call this from a healing pickup, a potion, a checkpoint, etc.
    public void RestoreHealth(int amount)
    {
        _currentHealth += amount;
        healthBar.Heal(amount);
    }

    // Example: snapping directly to a known value, e.g. respawning at full HP,
    // or a boss fight scripted event that sets health to an exact number.
    public void ResetToFull()
    {
        _currentHealth = maxHealth;
        healthBar.SetHealth(_currentHealth);
    }

    private void Die()
    {
        Debug.Log("Player died — TODO: trigger death sequence");
    }

    // --- Example trigger, just to show TakeDamage being called from gameplay ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hazard"))
        {
            TakeDamage(2); // one heart's worth
        }
        else if (other.CompareTag("HealthPickup"))
        {
            RestoreHealth(2);
            Destroy(other.gameObject);
        }
    }
}
