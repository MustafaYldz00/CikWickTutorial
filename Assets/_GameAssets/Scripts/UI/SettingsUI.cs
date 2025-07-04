using DG.Tweening;
using MaskTransitions;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _settingPopupObject;
    [SerializeField] private GameObject _blackBackgroundObject;

    [Header("Buttons")]
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;

    [Header("Sprites")]
    [SerializeField] private Sprite _soundActiveSptire;
    [SerializeField] private Sprite _soundPassiveSptire;
    [SerializeField] private Sprite _MusicActiveSptire;
    [SerializeField] private Sprite _MusicPassiveSptire;


    [Header("Settings")]
    [SerializeField] private float _animationDuration;

    private Image _blackBackgroundImage;

    [SerializeField] private bool _isMusicActive;
    [SerializeField] private bool _isSoundActive;

    private void Awake()
    {
        _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
        _settingPopupObject.transform.localScale = Vector3.zero;
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);

        _mainMenuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound);
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE);
        });

        _musicButton.onClick.AddListener(OnMusicClickek);
        _soundButton.onClick.AddListener(OnSoundClickek);
    }

    private void OnSoundClickek()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        _isSoundActive = !_isSoundActive;
        _soundButton.image.sprite = _isSoundActive ? _soundActiveSptire : _soundPassiveSptire; //isSound true ise active false ise passive olacak // Ternary Operator
        AudioManager.Instance.SetSoundEffectsMute(!_isSoundActive);

    }

    private void OnMusicClickek()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        _isMusicActive = !_isMusicActive;
        _musicButton.image.sprite = _isMusicActive ? _MusicActiveSptire : _MusicPassiveSptire;
        BackgroundMusic.Instance.SetMusicMute(!_isMusicActive);

    }

    private void OnSettingsButtonClicked()
    {
        GameManager.Instance.ChangeGameState(GameState.Pause);
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        _blackBackgroundObject.SetActive(true);
        _settingPopupObject.SetActive(true);

        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        _settingPopupObject.transform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }

    private void OnResumeButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        _blackBackgroundImage.DOFade(0f, _animationDuration).SetEase(Ease.Linear);
        _settingPopupObject.transform.DOScale(0f, _animationDuration).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            GameManager.Instance.ChangeGameState(GameState.Resume);
            _blackBackgroundObject.SetActive(false);
            _settingPopupObject.SetActive(false);
        });

    }
}
