using UnityEngine;

public class FireDamage : MonoBehaviour, IDamageables
{
    [SerializeField] private float _force = 10f;

    public void GiveDamage(Rigidbody playerRigidbody, Transform playerVisualTransform)
    {
        HealtManager.Instance.Damage(1);
        playerRigidbody.AddForce(-playerVisualTransform.forward * _force, ForceMode.Impulse);
        AudioManager.Instance.Play(SoundType.ChickSound);
        Destroy(gameObject);
    }
}
