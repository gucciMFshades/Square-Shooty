using UnityEngine;

public class XPCollectable : MonoBehaviour
{
    [SerializeField] private int xpValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            XPManager.Instance.AddXP(xpValue);
            Destroy(gameObject);
        }
    }
}
