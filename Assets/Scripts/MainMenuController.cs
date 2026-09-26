using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Drives a Control-style main menu built with UI Toolkit (MainMenu.uxml / MainMenu.uss).
/// Attach to a GameObject carrying a UIDocument component that points at MainMenu.uxml.
/// </summary>
[RequireComponent(typeof(UIDocument))]
public class MainMenuController : MonoBehaviour
{
    private const string SelectedClass = "menu-item--selected";
    private const string DisabledClass = "menu-item--disabled";

    // Order here must match the visual top-to-bottom order in the UXML.
    private readonly string[] _itemNames =
    {
        "item-new-game",
        "item-continue",
        "item-settings",
        "item-credits",
        "item-exit"
    };

    private UIDocument _document;
    private readonly List<VisualElement> _items = new List<VisualElement>();
    private int _selectedIndex = 0;

    // Wire these up from another script (game bootstrapper, scene loader, etc.)
    // or just fill them in directly below if this controller owns the logic.
    public event Action OnNewGame;
    public event Action OnContinue;
    public event Action OnSettings;
    public event Action OnCredits;
    public event Action OnExit;

    private bool _hasSaveFile = false; // toggle to enable/disable CONTINUE

    private void OnEnable()
    {
        _document = GetComponent<UIDocument>();
        var root = _document.rootVisualElement;

        _items.Clear();
        foreach (var name in _itemNames)
        {
            var element = root.Q<VisualElement>(name);
            if (element == null)
            {
                Debug.LogWarning($"MainMenuController: could not find element '{name}' in UXML.");
                continue;
            }
            _items.Add(element);

            // Mouse support: hover selects, click activates.
            int capturedIndex = _items.Count - 1;
            element.RegisterCallback<MouseEnterEvent>(_ => SetSelected(capturedIndex));
            element.RegisterCallback<ClickEvent>(_ => Activate(capturedIndex));
        }

        if (!_hasSaveFile)
        {
            SetDisabled("item-continue", true);
        }

        SetSelected(0);
        root.focusable = true;
        root.Focus();
        root.RegisterCallback<KeyDownEvent>(OnKeyDown);
    }

    private void OnDisable()
    {
        _document.rootVisualElement.UnregisterCallback<KeyDownEvent>(OnKeyDown);
    }

    private void OnKeyDown(KeyDownEvent evt)
    {
        switch (evt.keyCode)
        {
            case KeyCode.DownArrow:
            case KeyCode.S:
                MoveSelection(1);
                evt.StopPropagation();
                break;

            case KeyCode.UpArrow:
            case KeyCode.W:
                MoveSelection(-1);
                evt.StopPropagation();
                break;

            case KeyCode.Return:
            case KeyCode.KeypadEnter:
            case KeyCode.Space:
                Activate(_selectedIndex);
                evt.StopPropagation();
                break;

            case KeyCode.Escape:
                Activate(_items.Count - 1); // treat Escape as "exit" shortcut, optional
                evt.StopPropagation();
                break;
        }
    }

    private void MoveSelection(int delta)
    {
        int next = _selectedIndex;
        int attempts = 0;

        do
        {
            next = (next + delta + _items.Count) % _items.Count;
            attempts++;
        }
        while (IsDisabled(_items[next]) && attempts <= _items.Count);

        SetSelected(next);
    }

    private void SetSelected(int index)
    {
        if (index < 0 || index >= _items.Count) return;
        if (IsDisabled(_items[index])) return;

        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].EnableInClassList(SelectedClass, i == index);
        }

        _selectedIndex = index;
    }

    private void Activate(int index)
    {
        if (index < 0 || index >= _items.Count) return;
        if (IsDisabled(_items[index])) return;

        switch (_itemNames[index])
        {
            case "item-new-game":
                OnNewGame?.Invoke();
                break;
            case "item-continue":
                OnContinue?.Invoke();
                break;
            case "item-settings":
                OnSettings?.Invoke();
                break;
            case "item-credits":
                OnCredits?.Invoke();
                break;
            case "item-exit":
                OnExit?.Invoke();
                break;
        }
    }

    private void SetDisabled(string elementName, bool disabled)
    {
        var element = _document.rootVisualElement.Q<VisualElement>(elementName);
        if (element == null) return;
        element.EnableInClassList(DisabledClass, disabled);
    }

    private bool IsDisabled(VisualElement element)
    {
        return element.ClassListContains(DisabledClass);
    }
}
