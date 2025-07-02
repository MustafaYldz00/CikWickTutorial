using UnityEngine;
using UnityEngine.UI;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesingSO _wheatDesingSO;
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private PlayerStateUI _playerStateUI;

    private RectTransform _playerBoosterTransform;
    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterJumoTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }


    public void Collect()
    {
        _playerControl.SetJumpForce(_wheatDesingSO.IncreaseDecreaseMultiplier, _wheatDesingSO.ResetBoostDuration);

        _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage,
            _playerStateUI.GetHolyBoosterImage, _wheatDesingSO.ActiveSprite, _wheatDesingSO.PassiveSprite,
            _wheatDesingSO.ActiveWheatSprite, _wheatDesingSO.PassiveWHeatSprite, _wheatDesingSO.ResetBoostDuration);

        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);

        Destroy(gameObject);
    }
}
