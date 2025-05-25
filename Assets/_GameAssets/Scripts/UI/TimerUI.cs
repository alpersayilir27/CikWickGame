using System;
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
    private bool isTimerRunning;
    private Tween rotationTween;
    private string finalTime;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Play:
                PlayRotationAnimation();
                StartTimer();
                break;
            case GameState.Pause:
                StopTimer();
                break;
            case GameState.Resume:
                ResumeTimer();
                break;
            case GameState.GameOver:
                FinishTimer();
                break;
        }
    }

    private void PlayRotationAnimation()
    {
        rotationTween = timerRotatableTransform.DORotate(new Vector3(0, 0, -360), rotationDuration, RotateMode.FastBeyond360)
            .SetEase(rotationEase)
            .SetLoops(-1, LoopType.Incremental);
    }

    private void StartTimer()
    {
        isTimerRunning = true;
        elapsedTime = 0f;
        InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
    }

    private void StopTimer()
    {
        isTimerRunning = false;
        CancelInvoke(nameof(UpdateTimerUI));
        rotationTween.Pause();
    }

    private void ResumeTimer()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
            rotationTween.Play();
        }
    }

    private void FinishTimer()
    {
        StopTimer();
        finalTime = GetFormattedElapsedTime();
    }

    private string GetFormattedElapsedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateTimerUI()
    {
        if (!isTimerRunning)
        {
            return;
        }

        elapsedTime += 1f;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    public string GetFinalTime()
    {
        return finalTime;
    }
}
