using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private float _momementDecreaseSpeed;
    [SerializeField] private float _resetBoostDuration;

    public void Collect()
    {
        _playerControl.SetMovementSpeed(_momementDecreaseSpeed, _resetBoostDuration);
        Destroy(gameObject);
    }
}
