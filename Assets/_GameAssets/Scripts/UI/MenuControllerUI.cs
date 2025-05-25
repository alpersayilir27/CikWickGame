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
            SceneManager.LoadScene(Const.SceneNames.GAME_SCENE);
        });

        quitButton.onClick.AddListener(() =>
        {
            Debug.Log("Quit button clicked. Exiting the game.");
            Application.Quit();
        });
    }
}
