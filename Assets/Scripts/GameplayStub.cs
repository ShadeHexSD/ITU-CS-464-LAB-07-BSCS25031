using TMPro;
using UnityEngine;

// A fake "match": just a countdown, so you can see that Pause really stops time.
public class GameplayStub : MonoBehaviour
{
    [SerializeField] TMP_Text timerLabel;
    [SerializeField] float startSeconds = 134f;
    float remaining;

    void OnEnable() { ResetTimer(); }

    public void ResetTimer() { remaining = startSeconds; Show(); }

    void Update()
    {
        // Time.deltaTime is 0 while paused, so the clock freezes by itself.
        remaining = Mathf.Max(0f, remaining - Time.deltaTime);
        Show();
    }

    void Show()
    {
        int s = Mathf.CeilToInt(remaining);
        timerLabel.text = (s / 60) + ":" + (s % 60).ToString("00");
    }
}