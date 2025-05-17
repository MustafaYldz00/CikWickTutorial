using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesingSO _wheatDesingSO;
    [SerializeField] private PlayerControl _playerControl;
    

    public void Collect()
    {
        _playerControl.SetJumpForce(_wheatDesingSO.IncreaseDecreaseMultiplier, _wheatDesingSO.ReputationMultiplier);
        Destroy(gameObject);
    }
}
