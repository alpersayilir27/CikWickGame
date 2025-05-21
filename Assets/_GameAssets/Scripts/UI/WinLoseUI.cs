using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private GameObject blackBackgroundObject;
    [SerializeField] private GameObject winPopup;

    [SerializeField] private GameObject losePopup;

    [Header("Settings")]
    [SerializeField] private float animationDuration = 0.3f;

    private Image blackBackgroundImage;
    private RectTransform winPopupRectTransform;
    private RectTransform losePopupRectTransform;

    private void Awake()
    {
        blackBackgroundImage = blackBackgroundObject.GetComponent<Image>();
        winPopupRectTransform = winPopup.GetComponent<RectTransform>();
        losePopupRectTransform = losePopup.GetComponent<RectTransform>();


    }

    public void OnGameWin()
    {
        blackBackgroundObject.SetActive(true);
        winPopup.SetActive(true);

        blackBackgroundImage.DOFade(0.8f, animationDuration).SetEase(Ease.Linear);
        winPopupRectTransform.DOScale(1.5f, animationDuration).SetEase(Ease.OutBack);

    }
    
    public void OnGameLose()
    {
        blackBackgroundObject.SetActive(true);
        losePopup.SetActive(true);

        blackBackgroundImage.DOFade(0.8f, animationDuration).SetEase(Ease.Linear);
        losePopupRectTransform.DOScale(1.5f, animationDuration).SetEase(Ease.OutBack);

    }
}
