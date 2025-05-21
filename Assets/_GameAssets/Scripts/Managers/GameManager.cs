using System;
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
    private int currentEggCount;

    private GameState currentGameState;

    private void Awake()
    {
        Instance = this;
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

    public GameState GetCurentGameState()
    {
        return currentGameState;
    }
}
