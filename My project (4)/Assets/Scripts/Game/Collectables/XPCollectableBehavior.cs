using UnityEngine;

public class XPCollectableBehaviour : MonoBehaviour, ICollectableBehaviour
{
    [SerializeField] private int xpValue = 1;

    public void OnCollected(GameObject collector)
    {
        if (XPManager.Instance != null)
        {
            XPManager.Instance.AddXP(xpValue);
        }
        else
        {
            Debug.LogError("No XPManager instance found!");
        }
    }
}