using UnityEngine.UIElements;
 
public enum HudCorner
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}
 
/// <summary>
/// Positions a UI Toolkit element at a given screen corner with a margin,
/// overriding whatever static top/left/right/bottom is in the USS file.
/// Used so the same prefab (HealthBar, ScoreCounter, ...) can be placed
/// at a different corner per instance — e.g. Player 1 on the left,
/// Player 2 on the right — purely from the Inspector, no USS edits needed.
/// </summary>
public static class HudPositioning
{
    public static void Apply(VisualElement element, HudCorner corner, float marginX, float marginY)
    {
        if (element == null) return;
 
        element.style.position = Position.Absolute;
 
        // Clear all four first so switching corners at runtime doesn't leave stale values.
        element.style.top = StyleKeyword.Auto;
        element.style.bottom = StyleKeyword.Auto;
        element.style.left = StyleKeyword.Auto;
        element.style.right = StyleKeyword.Auto;
 
        switch (corner)
        {
            case HudCorner.TopLeft:
                element.style.top = marginY;
                element.style.left = marginX;
                break;
            case HudCorner.TopRight:
                element.style.top = marginY;
                element.style.right = marginX;
                break;
            case HudCorner.BottomLeft:
                element.style.bottom = marginY;
                element.style.left = marginX;
                break;
            case HudCorner.BottomRight:
                element.style.bottom = marginY;
                element.style.right = marginX;
                break;
        }
    }
}