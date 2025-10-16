using UnityEngine;
using UnityEngine.Events;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float _currentHealth = 3;
    [SerializeField] private float _maximumHealth = 3;

    public UnityEvent OnDied;
    public UnityEvent OnDamaged;
    public UnityEvent OnHealthChanged;

    public float RemainingHealthPercentage
    {
        get
        {
            return _currentHealth / _maximumHealth;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (_currentHealth == 0)
        {
            return; 
        }

        _currentHealth -= damageAmount;

        OnHealthChanged?.Invoke();

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnDied?.Invoke();
        }
        else
        {
            OnDamaged?.Invoke();
        }
    }
}