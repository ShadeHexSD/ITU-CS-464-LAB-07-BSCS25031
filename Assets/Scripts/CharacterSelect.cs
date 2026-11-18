using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Brawler
{
    public string displayName = "Bolt";
    [Range(0, 1)] public float health = 0.5f;
    [Range(0, 1)] public float damage = 0.5f;
    [Range(0, 1)] public float speed = 0.5f;
}

// Put on the Loadout screen's content. Slots, rings and avatars must be in the same order.
public class CharacterSelect : MonoBehaviour
{
    [SerializeField] Brawler[] brawlers;
    [SerializeField] Button[] slotButtons;
    [SerializeField] RectTransform[] avatars;     // the coloured circle in each slot
    [SerializeField] GameObject[] rings;          // the yellow selection ring in each slot
    [SerializeField] TMP_Text nameLabel;
    [SerializeField] Image[] statBars;            // Health, Damage, Speed (Image Type: Filled)

    int selected;
    Coroutine scaleRoutine, barRoutine;

    void Awake()
    {
        for (int i = 0; i < slotButtons.Length; i++)
        {
            int index = i;   // each button remembers its own number
            slotButtons[i].onClick.AddListener(() => Select(index));
        }
    }

    // Every time the screen opens the bars fill from empty: a small "hello".
    void OnEnable()
    {
        foreach (var bar in statBars) bar.fillAmount = 0f;
        Select(selected);
    }

    void Select(int index)
    {
        selected = index;
        nameLabel.text = brawlers[index].displayName;
        for (int i = 0; i < rings.Length; i++) rings[i].SetActive(i == index);

        // 1) The chosen avatar grows to 110%, the others go back to 100%.
        float[] from = new float[avatars.Length];
        for (int i = 0; i < avatars.Length; i++) from[i] = avatars[i].localScale.x;
        if (scaleRoutine != null) StopCoroutine(scaleRoutine);
        scaleRoutine = StartCoroutine(Tween.Run(0.30f, Ease.OutBack, p =>
        {
            for (int i = 0; i < avatars.Length; i++)
            {
                float s = Mathf.LerpUnclamped(from[i], i == index ? 1.1f : 1f, p);
                avatars[i].localScale = new Vector3(s, s, 1f);
            }
        }));

        // 2) The stat bars slide to the new values.
        Brawler b = brawlers[index];
        float[] target = { b.health, b.damage, b.speed };
        float[] start = new float[statBars.Length];
        for (int i = 0; i < statBars.Length; i++) start[i] = statBars[i].fillAmount;
        if (barRoutine != null) StopCoroutine(barRoutine);
        barRoutine = StartCoroutine(Tween.Run(0.55f, Ease.OutExpo, p =>
        {
            for (int i = 0; i < statBars.Length; i++)
                statBars[i].fillAmount = Mathf.Lerp(start[i], target[i], p);
        }));
    }
}