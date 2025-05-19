using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealtUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image[] _playerHealtImages;

    [Header("Sprites")]
    [SerializeField] private Sprite _playerHealthySprite;
    [SerializeField] private Sprite _playerUnhealthySprite;

    [Header("Settings")]
    [SerializeField] private float _scaleDuration;

    private RectTransform[] _playerHealtTransforms;

    private void Awake()
    {
        _playerHealtTransforms = new RectTransform[_playerHealtImages.Length];

        for (int i = 0; i < _playerHealtImages.Length; i++)
        {
            _playerHealtTransforms[i] = _playerHealtImages[i].gameObject.GetComponent<RectTransform>();
        }
    }

    public void AnimatedDamage()
    {
        for (int i = 0; i < _playerHealtImages.Length; i++)
        {
            if (_playerHealtImages[i].sprite == _playerHealthySprite)
            {
                AnimatedDamageSprite(_playerHealtImages[i], _playerHealtTransforms[i]);
                break;
            }
        }
    }

    public void AnimatedDamageForAll()
    {
        for (int i = 0; i < _playerHealtImages.Length; i++)
        {
            AnimatedDamageSprite(_playerHealtImages[i], _playerHealtTransforms[i]);
        }
    }

    //for testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AnimatedDamage();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            AnimatedDamageForAll();
        }
    }

    private void AnimatedDamageSprite(Image activeImage, RectTransform activeImageTransform)
    {
        activeImageTransform.DOScale(0f, _scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            activeImage.sprite = _playerUnhealthySprite;
            activeImageTransform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack);
        });
    }
}
