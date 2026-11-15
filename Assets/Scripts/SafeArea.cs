using UnityEngine;

// Put this on the 'Safe' child of every screen. It shrinks that child to the
// part of the phone screen that is not covered by a notch or the home bar.
public class SafeArea : MonoBehaviour
{
    RectTransform rect;
    Rect lastSafeArea;

    void Awake() { rect = (RectTransform)transform; Apply(); }

    void Update()
    {
        if (Screen.safeArea != lastSafeArea) Apply();   // also handles rotating the phone
    }

    void Apply()
    {
        lastSafeArea = Screen.safeArea;
        Vector2 min = lastSafeArea.position;
        Vector2 max = lastSafeArea.position + lastSafeArea.size;
        min.x /= Screen.width; min.y /= Screen.height;
        max.x /= Screen.width; max.y /= Screen.height;
        rect.anchorMin = min;
        rect.anchorMax = max;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }
}