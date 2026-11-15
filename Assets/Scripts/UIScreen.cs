using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Put this on the root object of every menu screen.
[RequireComponent(typeof(CanvasGroup))]
public class UIScreen : MonoBehaviour
{
    [Tooltip("The button the keyboard/gamepad starts on when this screen opens.")]
    public GameObject firstSelected;

    [Tooltip("Tick for screens where the Esc / Android back key must NOT go back (Match, Results).")]
    public bool ignoreBackKey;

    // Other scripts can listen to this to play an intro animation.
    public event Action Shown;

    CanvasGroup group;
    RectTransform rect;

    public CanvasGroup Group { get { if (group == null) group = GetComponent<CanvasGroup>(); return group; } }
    public RectTransform Rect { get { if (rect == null) rect = (RectTransform)transform; return rect; } }

    // While a screen is moving we switch its buttons off so you cannot double tap.
    public void SetInteractable(bool on)
    {
        Group.interactable = on;
        Group.blocksRaycasts = on;
    }

    // Puts position, size and fade back to normal.
    public void ResetVisual()
    {
        Rect.anchoredPosition = Vector2.zero;
        Rect.localScale = Vector3.one;
        Group.alpha = 1f;
    }

    // Called by ScreenManager once the screen has fully arrived.
    public void NotifyShown()
    {
        if (firstSelected != null && EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(firstSelected);
        Shown?.Invoke();
    }
}