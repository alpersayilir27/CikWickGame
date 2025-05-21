using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePopup : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TimerUI timerUI;

    private void OnEnable()
    {
        timerText.text = timerUI.GetFinalTime();
        tryAgainButton.onClick.AddListener(OnOneMoreButtonClicked);
    }

    private void OnOneMoreButtonClicked()
    {
        SceneManager.LoadScene(Const.SceneNames.GAME_SCENE);
    }
}
