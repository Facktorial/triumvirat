using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] TextMeshProUGUI score1;
    [SerializeField] TextMeshProUGUI score2;

    [SerializeField] TextMeshProUGUI startCounter;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateScore(int score1, int score2)
    {
        this.score1.text = score1.ToString();
        this.score2.text = score2.ToString();
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
