using UnityEngine;

public class Collectable : MonoBehaviour
{
    private ICollectableBehaviour _collectableBehaviour;

    private void Awake()
    {
        _collectableBehaviour = GetComponent<ICollectableBehaviour>();
        if (_collectableBehaviour == null)
        {
            Debug.LogError("No ICollectableBehaviour found on " + gameObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMovement>();

        if (player != null && _collectableBehaviour != null)
        {
            _collectableBehaviour.OnCollected(player.gameObject);
            Destroy(gameObject);
        }
    }
}