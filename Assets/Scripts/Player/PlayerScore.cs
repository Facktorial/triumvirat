using UnityEngine;

/// <summary>
/// Owns a player's score state and keeps the score HUD in sync.
/// </summary>
public class PlayerScore : MonoBehaviour
{
    private const int MinScore = 0;
    private const int MaxScore = 1000;

    [SerializeField] private ScoreCounterController scoreCounter;
    [SerializeField] private int currentScore;

    public int CurrentScore => currentScore;

    private void Start()
    {
        ApplyHudState();
    }

    public void Configure(ScoreCounterController hud, int startingScore)
    {
        scoreCounter = hud;
        currentScore = Mathf.Clamp(startingScore, MinScore, MaxScore);
        ApplyHudState();
    }

    public void SetScore(int newScore)
    {
        currentScore = Mathf.Clamp(newScore, MinScore, MaxScore);
        ApplyHudState();
    }

    public void Add(int incScore)
    {
        SetScore(currentScore + incScore);
    }

    public void ResetToZero()
    {
        SetScore(0);
    }

    private void ApplyHudState()
    {
        if (scoreCounter != null)
        {
            scoreCounter.SetScore(currentScore);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        currentScore = Mathf.Clamp(currentScore, MinScore, MaxScore);

        if (Application.isPlaying)
        {
            ApplyHudState();
        }
    }
#endif
}