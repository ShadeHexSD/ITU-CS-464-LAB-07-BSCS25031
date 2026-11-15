using UnityEngine;

// Fades an object in and slides it up a little each time its screen opens.
// Put it on a child that also has a CanvasGroup.
[RequireComponent(typeof(CanvasGroup))]
public class FadeUpOnShown : MonoBehaviour
{
    [SerializeField] UIScreen screen;      // the screen this belongs to
    [SerializeField] float rise = 60f;

    CanvasGroup group;
    RectTransform rect;
    Vector2 home;

    void Awake()
    {
        group = GetComponent<CanvasGroup>();
        rect = (RectTransform)transform;
        home = rect.anchoredPosition;
        screen.Shown += Play;
    }

    void OnDestroy() { screen.Shown -= Play; }

    void Play()
    {
        StopAllCoroutines();
        StartCoroutine(Tween.Run(0.6f, Ease.OutCubic, p =>
        {
            group.alpha = p;
            rect.anchoredPosition = home + new Vector2(0f, Mathf.Lerp(-rise, 0f, p));
        }));
    }
}