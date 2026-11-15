using TMPro;
using UnityEngine;

// Put on a currency pill. Shows the coin or gem count and bounces when it changes.
public class CurrencyLabel : MonoBehaviour
{
    enum Kind { Coins, Gems }

    [SerializeField] Kind kind;
    [SerializeField] TMP_Text label;

    void OnEnable() { Wallet.Changed += OnChanged; Refresh(); }
    void OnDisable() { Wallet.Changed -= OnChanged; }

    void Refresh() { label.text = (kind == Kind.Coins ? Wallet.Coins : Wallet.Gems).ToString(); }

    void OnChanged()
    {
        Refresh();
        if (isActiveAndEnabled) StartCoroutine(Bounce());
    }

    System.Collections.IEnumerator Bounce()
    {
        // Grow to 125% and settle back: "your money just changed".
        yield return Tween.Run(0.35f, Ease.OutBack, p =>
        {
            float s = Mathf.LerpUnclamped(1.25f, 1f, p);
            transform.localScale = new Vector3(s, s, 1f);
        });
    }
}