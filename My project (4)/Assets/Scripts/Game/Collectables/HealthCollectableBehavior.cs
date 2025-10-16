using UnityEngine;

public class HealthCollectableBehavior : MonoBehaviour, ICollectableBehaviour
{
    [SerializeField] private int healthAmount = 50;

    public void OnCollected(GameObject player)
    {
        if (player.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.AddHealth(healthAmount);
        }
    }
}
