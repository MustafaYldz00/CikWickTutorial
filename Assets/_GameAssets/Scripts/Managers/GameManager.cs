using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action<GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] private EggCounterUI _eggCounterUI;
    [SerializeField] private WinLoseUI _winLoseUI;
    [SerializeField] private CatController _catController;
    [SerializeField] private PlayerHealtUI _playerHealtUI;
    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;
    [SerializeField] private float _delay;

    private int _currentEggCount;
    private bool _isCatCathed;

    private GameState _currentGameState;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HealtManager.Instance.OnPlayerDeath += HealtManager_OnPlayerDeath;
        _catController.OnCatCatched += CatController_OnCatCatched;
    }

    private void CatController_OnCatCatched()
    {
        if (!_isCatCathed)
        {
            _playerHealtUI.AnimatedDamageForAll();
            StartCoroutine(OnGameOver(true));
            CameraShake.Instance.ShakeCamera(1.5f, 1f, 0.2f);
            _isCatCathed = true;
        }
    }

    private void HealtManager_OnPlayerDeath()
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
        _currentGameState = gameState;
        Debug.Log("Game State: " + gameState);
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

    private IEnumerator OnGameOver(bool isCatCathed)
    {
        yield return new WaitForSeconds(_delay);
        ChangeGameState(GameState.GameOver);
        _winLoseUI.OnGameLose();
        if (isCatCathed)
        {
        AudioManager.Instance.Play(SoundType.CatSound);
        }
    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
}
