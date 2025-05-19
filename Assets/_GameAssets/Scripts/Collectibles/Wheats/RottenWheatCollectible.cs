using UnityEngine;
using UnityEngine.UI;

public class RottenWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private WheatDesingSO _wheatDesingSO;
    [SerializeField] private PlayerStateUI _playerStateUI;

    private RectTransform _playerBoosterTransform;
    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterSlowTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }

    public void Collect()
    {
        _playerControl.SetMovementSpeed(_wheatDesingSO.IncreaseDecreaseMultiplier, _wheatDesingSO.ResetBoostDuration);
        
        _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage,
            _playerStateUI.GetRottenBoosterImage, _wheatDesingSO.ActiveSprite, _wheatDesingSO.PassiveSprite,
            _wheatDesingSO.ActiveWheatSprite, _wheatDesingSO.PassiveWHeatSprite, _wheatDesingSO.ResetBoostDuration);
       
        Destroy(gameObject);
    }
}
