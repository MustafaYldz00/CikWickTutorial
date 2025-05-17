using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private WheatDesingSO _wheatDesingSO;

    public void Collect()
    {
        _playerControl.SetMovementSpeed(_wheatDesingSO.IncreaseDecreaseMultiplier, _wheatDesingSO.ReputationMultiplier);
        Destroy(gameObject);
    }
}
