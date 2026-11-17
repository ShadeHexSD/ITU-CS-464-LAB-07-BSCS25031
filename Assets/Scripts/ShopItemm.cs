using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Put this on a Slider. It shows the value as a percentage and
// makes the handle grow while you drag, then bounce back when you let go.
// There is no audio yet: it only reads and shows the value.
[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] TMP_Text valueLabel;   // the "80%" text next to the slider
    [SerializeField] RectTransform handle;  // the round knob you drag

    Slider slider;
    Coroutine running;

    // Other scripts can read this later when you add audio (0 = silent, 1 = full).
    public float Value => slider.value;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(Refresh);   // update the text whenever it moves
        Refresh(slider.value);
    }

    void Refresh(float value)
    {
        int percent = Mathf.RoundToInt(value * 100f);
        valueLabel.text = percent == 0 ? "Off" : percent + "%";   // a word, not just a position
    }

    // Finger down: the handle grows so you can see it is held.
    public void OnPointerDown(PointerEventData e) { ScaleHandle(1.25f, 0.15f, Ease.OutCubic); }

    // Finger up: the handle springs back with a small bounce.
    public void OnPointerUp(PointerEventData e) { ScaleHandle(1f, 0.30f, Ease.OutBack); }

    void ScaleHandle(float target, float duration, Ease ease)
    {
        if (running != null) StopCoroutine(running);
        float start = handle.localScale.x;
        running = StartCoroutine(Tween.Run(duration, ease, p =>
        {
            float s = Mathf.LerpUnclamped(start, target, p);
            handle.localScale = new Vector3(s, s, 1f);
        }));
    }
}