using DG.Tweening;
using TMPro;
using UnityEngine;

public class EggCounterUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text eggCounterText;

    [Header("Settings")]
    [SerializeField] private Color eggCounterColor;
    [SerializeField] private float colorDuration;
    [SerializeField] private float scaleDuration;
    private RectTransform eggCounterRectTransform;

    private void Awake()
    {
        eggCounterRectTransform = eggCounterText.gameObject.GetComponent<RectTransform>();
    }

    public void SetEggCounterText(int counter, int max)
    {
        eggCounterText.text = counter.ToString() + "/" + max.ToString();
    }

    public void SetEggCompleted()
    {
        eggCounterText.DOColor(eggCounterColor, colorDuration);
        eggCounterRectTransform.DOScale(1.2f, scaleDuration).SetEase(Ease.OutBack);
    
            
    }
}
