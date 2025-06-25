using UnityEngine;

public class CatStateController : MonoBehaviour
{
    [SerializeField] private CatState _CurrentCatState = CatState.Walking;

    private void Start()
    {
        ChangeState(CatState.Walking);
    }

    public void ChangeState(CatState newCatState)
    {
        if (_CurrentCatState == newCatState) { return; }
        _CurrentCatState = newCatState;
    }

    public CatState GetCurrentState()
    {
        return _CurrentCatState;
    }
}
