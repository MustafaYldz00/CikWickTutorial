using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerState _CurrentplayerState = PlayerState.Idle;

    private void Start()
    {
        ChangeState(PlayerState.Idle);
    }

    public void ChangeState(PlayerState newPlayerState)
    {
        if (newPlayerState == _CurrentplayerState) { return; }
 
        _CurrentplayerState = newPlayerState;
    }

    public PlayerState GetCurrentState()
    {
        return _CurrentplayerState;
    }
}
