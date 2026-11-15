using UnityEngine;

// Runs only while the title screen is visible.
public class TitleScreen : MonoBehaviour
{
    [SerializeField] RectTransform logo;
    [SerializeField] CanvasGroup prompt;     // the "Tap to start" text

    void Update()
    {
        float t = Time.unscaledTime;
        float wave = Mathf.Sin(t * 2.6f);    // a sine wave is a smooth ease-in-out that loops
        logo.localRotation = Quaternion.Euler(0f, 0f, wave * 3f);
        logo.localScale = Vector3.one * (1f + wave * 0.03f);
        prompt.alpha = Mathf.Lerp(0.35f, 1f, (Mathf.Sin(t * 4.5f) + 1f) * 0.5f);
    }
}