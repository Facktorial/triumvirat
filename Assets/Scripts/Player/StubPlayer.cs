using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

/// <summary>
/// Minimal stand-in for a real player — just enough to poke a HealthBar
/// and ScoreCounter instance with keyboard input, so you can verify two
/// independent HUDs (Player 1 / Player 2) work side by side before any
/// real gameplay/damage/scoring systems exist.
///
/// Put two of these in the scene (e.g. "StubPlayer1", "StubPlayer2"),
/// each with different key bindings, each pointing at its own
/// HealthBar/ScoreCounter prefab instance.
/// </summary>
public class StubPlayer : MonoBehaviour
{
    private enum BindingPreset
    {
        AutoByName,
        Player1,
        Player2,
        Custom,
    }

    [Header("HUD references — this player's own instances")]
    [SerializeField] private HealthBarController healthBar;
    [SerializeField] private ScoreCounterController scoreCounter;

    [Header("Bindings")]
    [SerializeField] private BindingPreset bindingPreset = BindingPreset.AutoByName;
    [SerializeField] private KeyCode damageKey = KeyCode.Q;
    [SerializeField] private KeyCode healKey = KeyCode.E;
    [SerializeField] private KeyCode decreaseScoreKey = KeyCode.A;
    [SerializeField] private KeyCode increaseScoreKey = KeyCode.D;
    [SerializeField] private KeyCode resetKey = KeyCode.None;

    [Header("Test amounts")]
    [SerializeField] private int healthAmount = 1;
    [SerializeField] private int scoreAmount = 10;
    [SerializeField] private int maxHealth = 5;

    public int HealthAmount
    {
        get => healthAmount;
        set => healthAmount = Mathf.Max(1, value);
    }

    public int ScoreAmount
    {
        get => scoreAmount;
        set => scoreAmount = Mathf.Max(1, value);
    }

    public int CurrentHealth => _currentHealth;
    public int CurrentScore => _currentScore;

    private int _currentHealth;
    private int _currentScore;

    private void Awake()
    {
        ApplyBindingPreset();
        ClampValues();
        _currentHealth = maxHealth;
        _currentScore = 0;
    }

    private void Start()
    {
        ApplyHudState();
    }

    private void Update()
    {
        if (WasPressedThisFrame(damageKey))
        {
            AddLives(-healthAmount);
        }

        if (WasPressedThisFrame(healKey))
        {
            AddLives(healthAmount);
        }

        if (WasPressedThisFrame(decreaseScoreKey))
        {
            AddScore(-scoreAmount);
        }

        if (WasPressedThisFrame(increaseScoreKey))
        {
            AddScore(scoreAmount);
        }

        if (resetKey != KeyCode.None && WasPressedThisFrame(resetKey))
        {
            ResetState();
        }
    }

    public void AddLives(int incLives)
    {
        SetLives(_currentHealth + incLives);
    }

    public void AddScore(int incScore)
    {
        SetScore(_currentScore + incScore);
    }

    private void ResetState()
    {
        _currentHealth = maxHealth;
        _currentScore = 0;
        ApplyHudState();
    }

    public void SetLives(int newLives)
    {
        _currentHealth = Mathf.Clamp(newLives, 0, maxHealth);
        ApplyHudState();
    }

    public void SetScore(int newScore)
    {
        _currentScore = Mathf.Clamp(newScore, 0, 1000);
        ApplyHudState();
    }

    private void ApplyHudState()
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(_currentHealth);
        }

        if (scoreCounter != null)
        {
            scoreCounter.SetScore(_currentScore);
        }
    }

    private void ApplyBindingPreset()
    {
        if (bindingPreset == BindingPreset.Custom)
        {
            return;
        }

        bool usePlayerTwoBindings = bindingPreset == BindingPreset.Player2;
        if (bindingPreset == BindingPreset.AutoByName)
        {
            usePlayerTwoBindings = gameObject.name.Contains("2");
        }

        if (usePlayerTwoBindings)
        {
            damageKey = KeyCode.I;
            healKey = KeyCode.O;
            decreaseScoreKey = KeyCode.K;
            increaseScoreKey = KeyCode.L;
        }
        else
        {
            damageKey = KeyCode.Q;
            healKey = KeyCode.E;
            decreaseScoreKey = KeyCode.A;
            increaseScoreKey = KeyCode.D;
        }

        maxHealth = 5;
    }

    private void ClampValues()
    {
        healthAmount = Mathf.Max(1, healthAmount);
        scoreAmount = Mathf.Max(1, scoreAmount);
        maxHealth = Mathf.Clamp(maxHealth, 1, 5);
    }

    private static bool WasPressedThisFrame(KeyCode key)
    {
        bool wasPressed = Input.GetKeyDown(key);

#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null)
        {
            wasPressed |= key switch
            {
                KeyCode.Q => Keyboard.current.qKey.wasPressedThisFrame,
                KeyCode.E => Keyboard.current.eKey.wasPressedThisFrame,
                KeyCode.A => Keyboard.current.aKey.wasPressedThisFrame,
                KeyCode.D => Keyboard.current.dKey.wasPressedThisFrame,
                KeyCode.I => Keyboard.current.iKey.wasPressedThisFrame,
                KeyCode.O => Keyboard.current.oKey.wasPressedThisFrame,
                KeyCode.K => Keyboard.current.kKey.wasPressedThisFrame,
                KeyCode.L => Keyboard.current.lKey.wasPressedThisFrame,
                KeyCode.Alpha1 => Keyboard.current.digit1Key.wasPressedThisFrame,
                KeyCode.Alpha2 => Keyboard.current.digit2Key.wasPressedThisFrame,
                KeyCode.Alpha3 => Keyboard.current.digit3Key.wasPressedThisFrame,
                KeyCode.Alpha4 => Keyboard.current.digit4Key.wasPressedThisFrame,
                _ => false,
            };
        }
#endif

        return wasPressed;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        ApplyBindingPreset();
        ClampValues();

        if (Application.isPlaying)
        {
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
            _currentScore = Mathf.Clamp(_currentScore, 0, 1000);
            ApplyHudState();
        }
    }
#endif
}
