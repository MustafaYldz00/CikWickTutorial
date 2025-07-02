using System;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.Playables;

public class PlayerStateUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private RectTransform _playerWalkingTransform;
    [SerializeField] private RectTransform _playerSlidingTransform;
    [SerializeField] private RectTransform _boosterSpeedTransform;
    [SerializeField] private RectTransform _boosterJumpTransform;
    [SerializeField] private RectTransform _boosterSlowTransform;
    [SerializeField] private PlayableDirector _playableDirector;

    [Header("Images")]
    [SerializeField] private Image _goldBoosterWheatImage;
    [SerializeField] private Image _holyBoosterWheatImage;
    [SerializeField] private Image _rottenBoosterWheatImage;

    [Header("Sprites")]
    [SerializeField] private Sprite _playerWalkingActiveSprite;
    [SerializeField] private Sprite _playerWalkingPassiveSprite;
    [SerializeField] private Sprite _playerSlidingActiveSprite;
    [SerializeField] private Sprite _playerSlidingPassiveSprite;

    [Header("Settings")]
    [SerializeField] private float _moveDuration;
    [SerializeField] private Ease _moveEase;

    public RectTransform GetBoosterSpeedTransform => _boosterSpeedTransform;
    public RectTransform GetBoosterJumoTransform => _boosterJumpTransform;
    public RectTransform GetBoosterSlowTransform => _boosterSlowTransform;
    public Image GetGoldBoosterImage => _goldBoosterWheatImage;
    public Image GetHolyBoosterImage => _holyBoosterWheatImage;
    public Image GetRottenBoosterImage => _rottenBoosterWheatImage;

    private Image _playerWalkingImage;
    private Image _playerSlidingImage;

    private void Awake()
    {
        _playerWalkingImage = _playerWalkingTransform.GetComponent<Image>();
        _playerSlidingImage = _playerSlidingTransform.GetComponent<Image>();
    }

    private void Start()
    {
        _playerControl.OnPlayerStateChanged += PlayerControl_OnPlayerStateChanged;

        _playableDirector.stopped += OnTimelineFinished;

        
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        SetStateUserInterfaces(_playerWalkingActiveSprite, _playerSlidingPassiveSprite,
            _playerWalkingTransform, _playerSlidingTransform);
    }

    private void PlayerControl_OnPlayerStateChanged(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                SetStateUserInterfaces(_playerWalkingActiveSprite,_playerSlidingPassiveSprite,_playerWalkingTransform, _playerSlidingTransform);
                break;

            case PlayerState.SlideIdle:
            case PlayerState.Slide:
                SetStateUserInterfaces(_playerWalkingPassiveSprite, _playerSlidingActiveSprite, _playerSlidingTransform, _playerWalkingTransform);
                break;
        }
    }

    private void SetStateUserInterfaces(Sprite playerWalkingSprite, Sprite playerSlidingSprite,
        RectTransform activeTransform, RectTransform passiveTransform)
    {
        _playerWalkingImage.sprite = playerWalkingSprite;
        _playerSlidingImage.sprite = playerSlidingSprite;

        activeTransform.DOAnchorPosX(-25f, _moveDuration).SetEase(_moveEase);
        passiveTransform.DOAnchorPosX(-90f, _moveDuration).SetEase(_moveEase);
    }

    private IEnumerator SetBoosterInterfaces(RectTransform activeTransform, Image boosterImage,
        Image wheatImage, Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite,
        Sprite passiveWheatSprite, float duration)
    {
        boosterImage.sprite = activeSprite;
        wheatImage.sprite = activeWheatSprite;

        activeTransform.DOAnchorPosX(25f, _moveDuration).SetEase(_moveEase);

        yield return new WaitForSeconds(duration);

        boosterImage.sprite = passiveSprite;
        wheatImage.sprite = passiveWheatSprite;
        activeTransform.DOAnchorPosX(90f, _moveDuration).SetEase(_moveEase);
    }
    
    public void PlayBoosterUIAnimations(RectTransform activeTransform, Image boosterImage,
        Image wheatImage, Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite,
        Sprite passiveWheatSprite, float duration)
    {
        StartCoroutine(SetBoosterInterfaces(activeTransform, boosterImage, wheatImage, activeSprite,
            passiveSprite, activeWheatSprite, passiveWheatSprite, duration));
    }

}
