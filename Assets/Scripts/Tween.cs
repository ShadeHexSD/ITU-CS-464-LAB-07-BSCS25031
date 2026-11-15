using System;
using System.Collections;
using UnityEngine;

// The easing curves we use across the whole project.
public enum Ease { Linear, OutCubic, InCubic, OutExpo, OutBack, InOutQuad }

// A tiny tween helper. No plugins needed.
public static class Tween
{
    const float C1 = 1.70158f;   // how far OutBack overshoots
    const float C3 = C1 + 1f;

    // Turns a straight 0..1 progress into a curved 0..1 progress.
    public static float Evaluate(Ease ease, float t)
    {
        t = Mathf.Clamp01(t);
        switch (ease)
        {
            case Ease.OutCubic: return 1f - Mathf.Pow(1f - t, 3f);
            case Ease.InCubic: return t * t * t;
            case Ease.OutExpo: return t >= 1f ? 1f : 1f - Mathf.Pow(2f, -10f * t);
            case Ease.OutBack: return 1f + C3 * Mathf.Pow(t - 1f, 3f) + C1 * Mathf.Pow(t - 1f, 2f);
            case Ease.InOutQuad:
                return t < 0.5f ? 2f * t * t : 1f - Mathf.Pow(-2f * t + 2f, 2f) / 2f;
            default: return t;
        }
    }

    // Runs for 'duration' seconds and calls onUpdate with the curved progress each frame.
    // Uses unscaled time so it keeps working when Time.timeScale is 0 (paused).
    // NOTE: OutBack can go above 1 for a moment. Use Mathf.LerpUnclamped with it.
    public static IEnumerator Run(float duration, Ease ease, Action<float> onUpdate)
    {
        if (duration <= 0f) { onUpdate(1f); yield break; }
        float time = 0f;
        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            onUpdate(Evaluate(ease, time / duration));
            yield return null;
        }
        onUpdate(1f);   // always finish exactly on the target
    }
}