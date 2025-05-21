using System;
using UnityEngine;

public class HealtManager : MonoBehaviour
{
    public static HealtManager Instance { get; private set; }

    public event Action OnPlayerDeath; 

    [SerializeField] private PlayerHealtUI _playerHealtUI;
    [SerializeField] private int _maxHealt = 3;

    private int _currentHealt;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentHealt = _maxHealt;
    }

    public void Damage(int damage)
    {
        if (_currentHealt > 0)
        {
            _currentHealt -= damage;
            _playerHealtUI.AnimatedDamage();

            if (_currentHealt <= 0)
            {
               OnPlayerDeath?.Invoke();
            }
        }
    }
    public void Heal(int healAmount)
    {
        if (_currentHealt < _maxHealt)
        {
            _currentHealt = Mathf.Min(_currentHealt + healAmount, _maxHealt);
        }
    }
}
