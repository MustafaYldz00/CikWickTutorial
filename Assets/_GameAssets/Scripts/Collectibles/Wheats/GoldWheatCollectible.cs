using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private float _momementIncreaseSpeed;
    [SerializeField] private float _resetBoostDuration;

    public void Collect()
    {
        _playerControl.SetMovementSpeed(_momementIncreaseSpeed, _resetBoostDuration);
        Destroy(gameObject);
    }
}
