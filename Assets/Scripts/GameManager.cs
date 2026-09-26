using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] List<Transform> players;
    [SerializeField] float gameStartTime;

    public int score1;
    public int score2;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RestartGame();
    }

    private void LateUpdate()
    {
        if (UIManager.Instance == null) return;

        UIManager.Instance.UpdateScore(score1, score2);

        Player player1 = GetPlayerById(1);
        Player player2 = GetPlayerById(2);

        UIManager.Instance.UpdateHealth(
            player1 != null ? player1.CurrentHealth : 0,
            player2 != null ? player2.CurrentHealth : 0);
    }

    public void AddScore(int id)
    {
        switch (id)
        {
            case 1:
                score2++; break;

            case 2:
                score1++; break;
        }
    }

    public void RestartGame()
    {
        foreach (Transform t in players)
        {
            t.GetComponent<Movement>().canMove = false;
        }

        StartCoroutine(StartGameRoutine());

        UIManager.Instance.StartCounter(gameStartTime);
        SpawnPlayers();
    }

    Player GetPlayerById(int id)
    {
        if (players == null) return null;

        foreach (Transform t in players)
        {
            if (t == null) continue;

            Player player = t.GetComponent<Player>();
            if (player != null && player.Id == id)
            {
                return player;
            }
        }

        return null;
    }

    void SpawnPlayers()
    {
        Transform point = null;

        foreach (Transform t in players) 
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Count)];

                if (point == spawn) continue;

                t.position = spawn.position;
                break;
            }
        }
    }

    IEnumerator StartGameRoutine()
    {
        yield return new WaitForSeconds(gameStartTime);

        foreach (Transform t in players)
        {
            t.GetComponent<Movement>().canMove = true;
        }
    }
}
