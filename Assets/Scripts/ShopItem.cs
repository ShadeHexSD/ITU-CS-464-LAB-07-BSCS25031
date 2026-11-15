using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Put on a shop card that has a buy Button and a price label.
public class ShopItem : MonoBehaviour
{
    enum Reward { Coins, Gems, Skin }

    [SerializeField] Reward reward = Reward.Coins;
    [SerializeField] int amount = 500;      // how many coins or gems you get
    [SerializeField] int gemCost = 0;       // 0 = free
    [SerializeField] bool oneTime = true;   // can only be bought once
    [SerializeField] Button buyButton;
    [SerializeField] TMP_Text priceLabel;

    bool owned;

    void Awake() { buyButton.onClick.AddListener(Buy); }

    void Buy()
    {
        if (owned) return;

        // Not enough gems: say so, tell them what to do, and do not take anything.
        if (gemCost > 0 && !Wallet.TrySpendGems(gemCost))
        {
            Toast.Instance.Show("Not enough gems. Try the gem pack.");
            return;
        }

        if (reward == Reward.Coins) Wallet.AddCoins(amount);
        if (reward == Reward.Gems) Wallet.AddGems(amount);

        if (oneTime)
        {
            owned = true;
            priceLabel.text = "Owned";
            buyButton.interactable = false;   // clearly finished, cannot press again
        }
        Toast.Instance.Show("Purchased!");   // same word the button promised
        StartCoroutine(Punch());
    }

    System.Collections.IEnumerator Punch()
    {
        // The label bounces so your eye goes to what changed.
        yield return Tween.Run(0.38f, Ease.OutBack, p =>
        {
            float s = Mathf.LerpUnclamped(1.3f, 1f, p);
            priceLabel.transform.localScale = new Vector3(s, s, 1f);
        });
    }
}