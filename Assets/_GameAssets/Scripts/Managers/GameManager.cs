using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public event Action<GameState> OnGameStateChanged;

    

    [Header("References")]

    [SerializeField] private EggCounterUI eggCounterUI;
    [SerializeField] private WinLoseUI winLoseUI;

    [SerializeField] private CatController catController;
    [SerializeField] private PlayerHealthUI playerHealthUI; 


    [Header("Settings")]

    [SerializeField] private int maxEggCount = 5;
    [SerializeField] private float delay;
    private int currentEggCount;
    private bool isCatCatched;

    private GameState currentGameState;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        HealthManager.Instance.OnPlayerDeath += HealthManager_OnPlayerDeath;
        catController.OnCatCatched += CatController_OnCatCathed;
    }

    private void CatController_OnCatCathed()
    {
        if (!isCatCatched)
        {
            playerHealthUI.AnimateDamageForAll();
            StartCoroutine(OnGameOver(true));
            CameraShake.Instance.ShakeCamera(1.5f, 1.25f, 0.5f);
            isCatCatched = true;
        }
    }

    private void HealthManager_OnPlayerDeath()
    {
        StartCoroutine(OnGameOver(false));
    }

    private void OnEnable()
    {
        ChangeGameState(GameState.CutScene);
        BackgroundMusic.Instance.PlayBackgroundMusic(true);
    }

    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        currentGameState = gameState;
        Debug.Log("Game State Changed: " + gameState);
    }

    public void OnEggCollected()
    {
        currentEggCount++;
        eggCounterUI.SetEggCounterText(currentEggCount, maxEggCount);
        if (currentEggCount == maxEggCount)
        {
            //WIN
            eggCounterUI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);
            winLoseUI.OnGameWin();
        }
    }

    private IEnumerator OnGameOver(bool isCatCatched)
    {

        yield return new WaitForSeconds(delay);
        ChangeGameState(GameState.GameOver);
        winLoseUI.OnGameLose();
        
        if (isCatCatched)
        {
            AudioManager.Instance.Play(SoundType.CatSound);   
        }
    }

    public GameState GetCurentGameState()
    {
        return currentGameState;
    }
}
