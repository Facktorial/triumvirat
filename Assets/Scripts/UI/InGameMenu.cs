
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    public void ContinueGame()
    {
        gameManager.ContinueGame();
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
