using UnityEngine;

public class XPSpawner : MonoBehaviour
{
    [SerializeField] private GameObject xpPrefab;

    public void SpawnXP()
    {
        Instantiate(xpPrefab, transform.position, Quaternion.identity);
    }
}