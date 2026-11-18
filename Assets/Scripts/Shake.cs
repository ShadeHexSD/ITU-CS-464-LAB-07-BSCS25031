using UnityEngine;

// Wiggles an object sideways: the "no" for a locked item. Call Play().
public class Shake : MonoBehaviour
{
    [SerializeField] float distance = 22f;
    Vector2 home;
    Coroutine running;

    void Awake() { home = ((RectTransform)transform).anchoredPosition; }

    public void Play()
    {
        if (running != null) StopCoroutine(running);
        running = StartCoroutine(Wiggle());
    }

    System.Collections.IEnumerator Wiggle()
    {
        var rect = (RectTransform)transform;
        yield return Tween.Run(0.30f, Ease.Linear, p =>
        {
            // 2 full back-and-forth swings that fade out as p goes to 1.
            float x = Mathf.Sin(p * Mathf.PI * 4f) * (1f - p) * distance;
            rect.anchoredPosition = home + new Vector2(x, 0f);
        });
        rect.anchoredPosition = home;
    }
}