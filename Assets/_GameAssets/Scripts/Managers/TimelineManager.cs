using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private PlayableDirector playableDirector;

    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    private void OnEnable()
    {
        playableDirector.Play();
        playableDirector.stopped += OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        gameManager.ChangeGameState(GameState.Play);
    }
}
