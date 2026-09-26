
using UnityEngine;
using UnityEngine.UIElements;
 
/// <summary>
/// Drives a 4-digit pixel-art score counter (ScoreCounter.uxml / ScoreCounter.uss).
/// Displays scores from 0 to 1000, zero-padded to 4 digits (e.g. "0042", "1000").
/// Attach to a GameObject with a UIDocument pointing at ScoreCounter.uxml.
/// </summary>
[RequireComponent(typeof(UIDocument))]
public class ScoreCounterController : MonoBehaviour
{
    private const int DigitCount = 4;
    private const int MinScore = 0;
    private const int MaxScore = 1000;
 
    [Header("Digit textures 0-9, in order")]
    [SerializeField] private Texture2D[] digitTextures = new Texture2D[10];
 
    [Header("Score")]
    [SerializeField] private int currentScore = 0;
 
    private UIDocument _document;
    private readonly VisualElement[] _digitSlots = new VisualElement[DigitCount];
 
    private void OnEnable()
    {
        _document = GetComponent<UIDocument>();
        var root = _document.rootVisualElement;
 
        for (int i = 0; i < DigitCount; i++)
        {
            _digitSlots[i] = root.Q<VisualElement>($"score-digit-{i}");
            if (_digitSlots[i] == null)
            {
                Debug.LogWarning($"ScoreCounterController: could not find element 'score-digit-{i}' in UXML.");
            }
        }
 
        Refresh();
    }
 
    /// <summary>Call this whenever the score changes.</summary>
    public void SetScore(int newScore)
    {
        currentScore = Mathf.Clamp(newScore, MinScore, MaxScore);
        Refresh();
    }
 
    public void AddScore(int amount) => SetScore(currentScore + amount);
 
    private void Refresh()
    {
        // e.g. 42 -> "0042", 1000 -> "1000"
        string padded = currentScore.ToString().PadLeft(DigitCount, '0');
 
        for (int i = 0; i < DigitCount; i++)
        {
            if (_digitSlots[i] == null) continue;
 
            int digitValue = padded[i] - '0';
            if (digitValue < 0 || digitValue > 9) continue;
 
            var texture = digitTextures[digitValue];
            _digitSlots[i].style.backgroundImage = new StyleBackground(texture);
        }
    }
 
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying && _document != null)
        {
            SetScore(currentScore);
        }
    }
#endif
}