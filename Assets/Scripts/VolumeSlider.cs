using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Put this on a Slider. Shows the value as a percentage; the handle grows
// while held and bounces back on release. No audio yet.
[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] TMP_Text valueLabel;
    [SerializeField] RectTransform handle;

    Slider slider;
    Coroutine running;

    public float Value => slider.value;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(Refresh);
        Refresh(slider.value);
    }

    void Refresh(float value)
    {
        int percent = Mathf.RoundToInt(value * 100f);
        valueLabel.text = percent == 0 ? "Off" : percent + "%";
    }

    public void OnPointerDown(PointerEventData e) { ScaleHandle(1.25f, 0.15f, Ease.OutCubic); }
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
