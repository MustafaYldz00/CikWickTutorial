using Unity.XR.Oculus.Input;
using UnityEngine;

public class PlayerIntrectionController : MonoBehaviour
{
    private PlayerControl _playerCOntrol;

    private void Awake()
    {
        _playerCOntrol = GetComponent<PlayerControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.Collect();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<IBoostable>(out var boostable))
        {
            boostable.Boost(_playerCOntrol);
        }
    }
}
