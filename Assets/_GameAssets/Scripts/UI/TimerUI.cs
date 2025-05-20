using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform timerRotatableTransform;
    [SerializeField] private TMP_Text timerText;

    [Header("Settings")]

    [SerializeField] private float rotationDuration;
    [SerializeField] private Ease rotationEase;

    private float elapsedTime;

    private void Start()
    {
        PlayRotationAnimation();
        StartTimer();
    }

    private void PlayRotationAnimation()
    {
        timerRotatableTransform.DORotate(new Vector3(0, 0, -360), rotationDuration, RotateMode.FastBeyond360)
            .SetEase(rotationEase)
            .SetLoops(-1, LoopType.Incremental);
    }

    private void StartTimer()
    {
        elapsedTime = 0f;
        InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f); 
    }

    private void UpdateTimerUI()
    {
        elapsedTime += 1f;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
