using UnityEngine;

/// <summary>
/// Example: a player health component that drives the HUD's HealthBarController.
/// Attach this to your Player GameObject and drag the HUD's HealthBarController
/// into the healthBar field in the Inspector.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    private const int MaxVisibleLives = 5;

    [SerializeField] private HealthBarController healthBar;
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth = 5;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    private void Awake()
    {
        ClampValues();
    }

    private void Start()
    {
        ClampValues();
        ApplyHudState();
    }

    public void Configure(HealthBarController hud, int startingMaxHealth)
    {
        healthBar = hud;
        maxHealth = Mathf.Clamp(startingMaxHealth, 1, MaxVisibleLives);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        ApplyHudState();
    }

    public void SetHealth(int newHealth)
    {
        currentHealth = Mathf.Clamp(newHealth, 0, maxHealth);
        ApplyHudState();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Add(int incLives)
    {
        SetHealth(currentHealth + incLives);
    }

    public void TakeDamage(int amount)
    {
        Add(-Mathf.Abs(amount));
    }

    public void RestoreHealth(int amount)
    {
        Add(Mathf.Abs(amount));
    }

    public void ResetToFull()
    {
        SetHealth(maxHealth);
    }

    private void ApplyHudState()
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
    }

    private void ClampValues()
    {
        maxHealth = Mathf.Clamp(maxHealth, 1, MaxVisibleLives);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
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
            TakeDamage(1);
        }
        else if (other.CompareTag("HealthPickup"))
        {
            RestoreHealth(1);
            Destroy(other.gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        ClampValues();

        if (Application.isPlaying)
        {
            ApplyHudState();
        }
    }
#endif
}
