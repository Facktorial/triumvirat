using UnityEngine;
using UnityEngine.UIElements;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
 
/// <summary>
/// Drives a 5-heart health bar (HealthBar.uxml / HealthBar.uss) in UI Toolkit.
/// Each heart represents 1 health point (max visible lives = <?>).
/// Attach to a GameObject with a UIDocument pointing at
/// HealthBar.uxml, or reuse the same UIDocument as your HUD if you merge the UXML in.
/// </summary>
[RequireComponent(typeof(UIDocument))]
public class HealthBarController : MonoBehaviour
{
    private const int HeartCount = 5;
 
    [Header("Textures — import as Sprite (2D and UI), Filter Mode: Point, no Compression")]
    [SerializeField] private Texture2D heartFull;
    [SerializeField] private Texture2D heartEmpty;
 
    [Header("Health")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth = 5;
 
    private UIDocument _document;
    private readonly VisualElement[] _hearts = new VisualElement[HeartCount];
 
    private void OnEnable()
    {
        _document = GetComponent<UIDocument>();
        var root = _document.rootVisualElement;
 
        for (int i = 0; i < HeartCount; i++)
        {
            _hearts[i] = root.Q<VisualElement>($"heart-{i}");
            if (_hearts[i] == null)
            {
                Debug.LogWarning($"HealthBarController: could not find element 'heart-{i}' in UXML.");
            }
        }
 
        Refresh();
    }

    private void Update()
    {
        bool damagePressed = Input.GetKeyDown(KeyCode.I);
        bool healPressed = Input.GetKeyDown(KeyCode.O);

#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null)
        {
            damagePressed |= Keyboard.current.iKey.wasPressedThisFrame;

            healPressed |= Keyboard.current.oKey.wasPressedThisFrame;
        }
#endif

        if (damagePressed)
        {
            Damage(1);
        }

        if (healPressed)
        {
            Heal(1);
        }
    }
 
    /// <summary>Call this whenever health changes (damage, healing, etc).</summary>
    public void SetHealth(int newHealth)
    {
        maxHealth = Mathf.Clamp(maxHealth, 0, HeartCount);
        currentHealth = Mathf.Clamp(newHealth, 0, maxHealth);
        Refresh();
    }
 
    public void Damage(int amount) => SetHealth(currentHealth - amount);
    public void Heal(int amount) => SetHealth(currentHealth + amount);
 
    private void Refresh()
    {
        for (int i = 0; i < HeartCount; i++)
        {
            if (_hearts[i] == null) continue;

            bool isVisibleHeart = i < maxHealth;
            bool isFilledHeart = i < currentHealth;

            _hearts[i].style.display = isVisibleHeart ? DisplayStyle.Flex : DisplayStyle.None;
            _hearts[i].style.backgroundImage = new StyleBackground(isFilledHeart ? heartFull : heartEmpty);
        }
    }
 
#if UNITY_EDITOR
    // Lets you drag the Current Health slider in the Inspector during Play Mode
    // and see the hearts update live, without needing a damage source hooked up yet.
    private void OnValidate()
    {
        maxHealth = Mathf.Clamp(maxHealth, 0, HeartCount);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (Application.isPlaying && _document != null)
        {
            Refresh();
        }
    }
#endif
}
