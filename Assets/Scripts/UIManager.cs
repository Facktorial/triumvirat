using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private ScoreCounterController scoreCounter1;
    [SerializeField] private ScoreCounterController scoreCounter2;

    [SerializeField] private HealthBarController healthBar1;
    [SerializeField] private HealthBarController healthBar2;

    [SerializeField] private TextMeshProUGUI startCounter;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateScore(int score1, int score2)
    {
        if (scoreCounter1 != null)
            scoreCounter1.SetScore(score1);

        if (scoreCounter2 != null)
            scoreCounter2.SetScore(score2);
    }

    public void UpdateHealth(int health1, int health2)
    {
        if (healthBar1 != null)
            healthBar1.SetHealth(health1);

        if (healthBar2 != null)
            healthBar2.SetHealth(health2);
    }

    public void StartCounter(float time)
    {
        StartCoroutine(CounterRoutine(time));
    }

    IEnumerator CounterRoutine(float time)
    {
        startCounter.gameObject.SetActive(true);

        time = time / 4;

        startCounter.text = "3";
        yield return new WaitForSeconds(time);

        startCounter.text = "2";
        yield return new WaitForSeconds(time);

        startCounter.text = "1";
        yield return new WaitForSeconds(time);

        startCounter.text = "START";
        yield return new WaitForSeconds(time);

        startCounter.gameObject.SetActive(false);
    }
}
