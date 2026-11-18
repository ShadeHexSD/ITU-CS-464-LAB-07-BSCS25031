using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Put this next to a Button. Pressing shrinks it, releasing springs it back.
public class ButtonFeedback : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] float pressedScale = 0.93f;

    Selectable selectable;
    Coroutine running;

    void Awake() { selectable = GetComponent<Selectable>(); }
    void OnDisable() { transform.localScale = Vector3.one; running = null; }

    public void OnPointerDown(PointerEventData e)
    {
        if (selectable != null && !selectable.interactable) return;
        ScaleTo(pressedScale, 0.08f, Ease.OutCubic);   // quick squash = "I felt that"
    }

    public void OnPointerUp(PointerEventData e) { ScaleTo(1f, 0.28f, Ease.OutBack); }     // bouncy release
    public void OnPointerExit(PointerEventData e) { ScaleTo(1f, 0.15f, Ease.OutCubic); }  // finger slid away

    void ScaleTo(float target, float duration, Ease ease)
    {
        if (!isActiveAndEnabled) return;
        if (running != null) StopCoroutine(running);
        float start = transform.localScale.x;
        running = StartCoroutine(Tween.Run(duration, ease, p =>
        {
            float s = Mathf.LerpUnclamped(start, target, p);
            transform.localScale = new Vector3(s, s, 1f);
        }));
    }
}