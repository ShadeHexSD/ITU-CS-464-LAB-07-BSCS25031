using UnityEngine;
using UnityEngine.UI;

// Put on a Button that has a round knob child. No audio or saving yet: it only remembers the state.
[RequireComponent(typeof(Button))]
public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] RectTransform knob;
    [SerializeField] Image background;
    [SerializeField] bool isOn = true;
    [SerializeField] float travel = 76f;     // how far the knob moves left to right
    [SerializeField] Color onColor = new Color32(0x3D, 0xDC, 0x84, 255);
    [SerializeField] Color offColor = new Color32(0x6B, 0x5B, 0xB0, 255);

    public bool IsOn => isOn;
    float position;   // 0 = off, 1 = on (can overshoot while bouncing)

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Flip);
        Apply(isOn ? 1f : 0f);
    }

    void Flip()
    {
        isOn = !isOn;
        float start = position;
        float end = isOn ? 1f : 0f;
        StopAllCoroutines();
        StartCoroutine(Tween.Run(0.30f, Ease.OutBack, p => Apply(Mathf.LerpUnclamped(start, end, p))));
    }

    void Apply(float value)
    {
        position = value;
        knob.anchoredPosition = new Vector2(Mathf.LerpUnclamped(-travel / 2f, travel / 2f, value), 0f);
        background.color = Color.Lerp(offColor, onColor, Mathf.Clamp01(value));
    }
}