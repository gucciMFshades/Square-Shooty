using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InvincibilityController))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maximumHealth = 10;
    [SerializeField] private int _currentHealth = 10;

    public int MaxHealth
    {
        get => _maximumHealth;
        set
        {
            _maximumHealth = value;
            if (_currentHealth > _maximumHealth) _currentHealth = _maximumHealth;
        }
    }

    public float RemainingHealthPercentage => (float)_currentHealth / _maximumHealth;

    public bool IsInvincible { get; private set; }

    public UnityEvent OnDied;
    public UnityEvent OnDamaged;
    public UnityEvent OnHealthChanged;

    private InvincibilityController _invincibilityController;

    private void Awake()
    {
        _invincibilityController = GetComponent<InvincibilityController>();
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0 || IsInvincible) return;

        
        _invincibilityController?.StartInvincibility(1f);

        _currentHealth -= damage;
        if (_currentHealth < 0) _currentHealth = 0;

        OnHealthChanged?.Invoke();

        if (_currentHealth == 0) OnDied?.Invoke();
        else OnDamaged?.Invoke();
    }

    public void AddHealth(int amount)
    {
        if (_currentHealth >= _maximumHealth) return;

        _currentHealth += amount;
        if (_currentHealth > _maximumHealth) _currentHealth = _maximumHealth;

        OnHealthChanged?.Invoke();
    }

    public void SetInvincible(bool value)
    {
        IsInvincible = value;
    }
}







