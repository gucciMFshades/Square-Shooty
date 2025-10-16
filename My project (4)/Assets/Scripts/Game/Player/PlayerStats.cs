using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [Header("Combat")]
    public float bulletDamage = 1f;   
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;     

    [Header("Health & Movement")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float moveSpeed = 5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
