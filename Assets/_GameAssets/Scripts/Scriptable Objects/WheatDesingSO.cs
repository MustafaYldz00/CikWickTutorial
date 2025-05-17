using UnityEngine;

[CreateAssetMenu(fileName = "WheatDesingSO", menuName = "ScriptableObjects/WheatDesingSO")]
public class WheatDesingSO : ScriptableObject
{
    [SerializeField] private float _increaseDecreaseMultiplier;
    [SerializeField] private float _reserBoostDuration;

    public float IncreaseDecreaseMultiplier => _increaseDecreaseMultiplier;
    public float ReputationMultiplier => _reserBoostDuration;

}
