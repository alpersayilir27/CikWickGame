using MaskTransitions;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControllerUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            TransitionManager.Instance.LoadLevel(Const.SceneNames.GAME_SCENE);
        });

        quitButton.onClick.AddListener(() =>
        {
            Debug.Log("Quit button clicked. Exiting the game.");
            Application.Quit();
        });
    }
}
