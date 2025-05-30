using System;
using DG.Tweening;
using MaskTransitions;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject settingsPopupObject;
    [SerializeField] private GameObject blackBackgroundObject;


    [Header("Buttons")]
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    [Header("Sprites")]
    [SerializeField] private Sprite musicActiveSprite;
    [SerializeField] private Sprite musicPassiveSprite;
    [SerializeField] private Sprite soundActiveSprite;
    [SerializeField] private Sprite soundPassiveSprite;

    [Header("Settings")]
    [SerializeField] private float animationDuration;

    private Image blackBackgroundImage;

    [SerializeField] private bool isMusicActive;
    [SerializeField] private bool isSoundActive;

    private void Awake()
    {
        blackBackgroundImage = blackBackgroundObject.GetComponent<Image>();
        settingsPopupObject.transform.localScale = Vector3.zero;
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        mainMenuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound);
            TransitionManager.Instance.LoadLevel(Const.SceneNames.MENU_SCENE);
        });

        musicButton.onClick.AddListener(OnMusicButtonClicked);
        soundButton.onClick.AddListener(OnSoundButtonClicked);
    }

    private void OnSoundButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        isSoundActive = !isSoundActive;
        soundButton.image.sprite = isSoundActive ? soundActiveSprite : soundPassiveSprite;
        AudioManager.Instance.SetSoundEffectsMute(!isSoundActive);
    }

    private void OnMusicButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        isMusicActive = !isMusicActive;
        musicButton.image.sprite = isMusicActive ? musicActiveSprite : musicPassiveSprite;
        BackgroundMusic.Instance.SetMusicMute(!isMusicActive);
    }

    private void OnSettingsButtonClicked()
    {
        GameManager.Instance.ChangeGameState(GameState.Pause);
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        blackBackgroundObject.SetActive(true);
        settingsPopupObject.SetActive(true);
        blackBackgroundImage.DOFade(0.8f, animationDuration).SetEase(Ease.Linear);
        settingsPopupObject.transform.DOScale(1.5f, animationDuration).SetEase(Ease.OutBack);
    }

    private void OnResumeButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.TransitionSound);
        blackBackgroundImage.DOFade(0f, animationDuration).SetEase(Ease.Linear);
        settingsPopupObject.transform.DOScale(0f, animationDuration).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            GameManager.Instance.ChangeGameState(GameState.Resume);
            settingsPopupObject.SetActive(false);
            blackBackgroundObject.SetActive(false);
        });
    }


}
