using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action <GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] private EggCounterUI _eggCounterUI;
    [SerializeField] private WinLoseUI _winLoseUI;

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;
    [SerializeField] private float _delay;

    private int _currentEggCount;
    private GameState _currentGameState;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HealtManager.Instance.OnPlayerDeath += HealtManager_OnPlayerDeath;
    }

    private void HealtManager_OnPlayerDeath()
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
        _currentGameState = gameState;
        Debug.Log("Game State: "+ gameState);
    }

    public void OnEggCollected()
    {
        _currentEggCount++;
        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount);

        if (_currentEggCount == _maxEggCount)
        {
            _eggCounterUI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);
            _winLoseUI.OnGameWin();
        }
    }

    private IEnumerator OnGameOver()
    {
        yield return new WaitForSeconds(_delay);
        ChangeGameState(GameState.GameOver);
        _winLoseUI.OnGameLose();
    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
}
