using MaskTransitions;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private Button oneMoreButton;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TimerUI timerUI;

    private void OnEnable()
    {
        timerText.text = timerUI.GetFinalTime();
        oneMoreButton.onClick.AddListener(OnOneMoreButtonClicked);
        MainMenuButton.onClick.AddListener(() =>
        {
            TransitionManager.Instance.LoadLevel(Const.SceneNames.MENU_SCENE);
        });
    }

    private void OnOneMoreButtonClicked()
    {
        TransitionManager.Instance.LoadLevel(Const.SceneNames.GAME_SCENE);
    }
}
