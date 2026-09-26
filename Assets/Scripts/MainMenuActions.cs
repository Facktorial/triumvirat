using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MainMenuController))]
public class MainMenuActions : MonoBehaviour
{
    private void OnEnable()
    {
        var menu = GetComponent<MainMenuController>();

        menu.OnNewGame += HandleNewGame;
        menu.OnContinue += HandleContinue;
        menu.OnSettings += HandleSettings;
        menu.OnCredits += HandleCredits;
        menu.OnExit += HandleExit;
    }

    private void OnDisable()
    {
        var menu = GetComponent<MainMenuController>();

        menu.OnNewGame -= HandleNewGame;
        menu.OnContinue -= HandleContinue;
        menu.OnSettings -= HandleSettings;
        menu.OnCredits -= HandleCredits;
        menu.OnExit -= HandleExit;
    }

    private void HandleNewGame() => SceneManager.LoadScene("Gameplay");
    private void HandleContinue() => Debug.Log("TODO: load save file");
    private void HandleSettings() => Debug.Log("TODO: open settings panel");
    private void HandleCredits() => Debug.Log("TODO: show credits screen");
    private void HandleExit() => Application.Quit();
}