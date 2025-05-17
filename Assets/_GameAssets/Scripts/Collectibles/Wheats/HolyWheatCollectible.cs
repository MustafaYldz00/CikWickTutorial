using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private float _forceIncrease;
    [SerializeField] private float _resetBoostDuration;

    public void Collect()
    {
        _playerControl.SetJumpForce(_forceIncrease, _resetBoostDuration);
        Destroy(gameObject);
    }
}
