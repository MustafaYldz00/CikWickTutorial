using System;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;

    private PlayableDirector _playableDirector;

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>(); 
    }

    private void OnEnable()
    {
        _playableDirector.Play();
        _playableDirector.stopped += OntimelineFinished;
    }

    private void OntimelineFinished(PlayableDirector director)
    {
        _gameManager.ChangeGameState(GameState.Play);
    }
}
