using UnityEngine;

public class SpatulaBooster : MonoBehaviour, IBoostable
{
    [Header("Settings")]
    [SerializeField] private float _jumpForce;
    
    [Header("References")]
    [SerializeField] private Animator _spatulaAnimator;

    private bool _isActivated;

    public void Boost(PlayerControl playerControl)
    {
        if (_isActivated) { return; }

        PlayBoostAnimation();
        Rigidbody playerRigidbody = playerControl.GetPlayerRigidbody();

        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0,playerRigidbody.linearVelocity.z);
        playerRigidbody.AddForce(transform.forward * _jumpForce, ForceMode.Impulse);
        _isActivated = true;
        Invoke(nameof(ResetActivation), 0.2f);
        AudioManager.Instance.Play(SoundType.SpatulaSound);
    }

    private void PlayBoostAnimation()
    {
        _spatulaAnimator.SetTrigger(Consts.OtherAnimations.IS_SPATULA_JUMPÝNG);
    }

    private void ResetActivation()
    {
        _isActivated = false;
    }
}
