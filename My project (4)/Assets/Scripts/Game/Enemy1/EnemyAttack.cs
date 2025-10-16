using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damagePerSecond = 10; 
    private float lastDamageTime;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            
            if (Time.time - lastDamageTime >= 1f)
            {
                playerHealth.TakeDamage(damagePerSecond);
                lastDamageTime = Time.time;
            }
        }
    }
}

