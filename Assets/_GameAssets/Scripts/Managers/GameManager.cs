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


    [Header("Settings")]

    [SerializeField] private int maxEggCount = 5;
    [SerializeField] private float delay;
    private int currentEggCount;

    private GameState currentGameState;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        HealthManager.Instance.OnPlayerDeath += HealthManager_OnPlayerDeath;
    }

    private void HealthManager_OnPlayerDeath()
    {
        StartCoroutine(OnGameOver());
    }

    private void OnEnable()
    {
        ChangeGameState(GameState.Play);
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

    private IEnumerator OnGameOver()
    {
        yield return new WaitForSeconds(delay);
        ChangeGameState(GameState.GameOver);
        winLoseUI.OnGameLose();
    }

    public GameState GetCurentGameState()
    {
        return currentGameState;
    }
}
