using Unity.XR.Oculus.Input;
using UnityEngine;

public class PlayerIntrectionController : MonoBehaviour
{
    [SerializeField] private Transform _playerVisualTransform;

    private PlayerControl _playerCOntrol;
    private Rigidbody _playerRigidbody;

    private void Awake()
    {
        _playerCOntrol = GetComponent<PlayerControl>();
        _playerRigidbody = GetComponent<Rigidbody>();
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

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<IDamageables>(out var damageables))
        {
            damageables.GiveDamage(_playerRigidbody, _playerVisualTransform);
        }
    }
}
