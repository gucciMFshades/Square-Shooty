using UnityEngine;
using UnityEngine.UI;

public class XPBarUI : MonoBehaviour
{
    [SerializeField] private Image _xpBarForegroundImage;
    [SerializeField] private float fillSpeed = 2f; 

    private float targetFill;
    private bool isFilling = false;

    private void Start()
    {
        if (XPManager.Instance != null)
        {
            XPManager.Instance.OnXPChanged.AddListener(OnXPChanged);
            targetFill = XPManager.Instance.RemainingXPPercentage;
            _xpBarForegroundImage.fillAmount = targetFill;
        }
        else
        {
            Debug.LogError("No XPManager instance in the scene!");
        }
    }

    private void Update()
    {
        if (isFilling)
        {
            _xpBarForegroundImage.fillAmount = Mathf.MoveTowards(
                _xpBarForegroundImage.fillAmount,
                targetFill,
                fillSpeed * Time.deltaTime
            );

            if (_xpBarForegroundImage.fillAmount == targetFill)
            {
                isFilling = false;
            }
        }
    }

    private void OnXPChanged()
    {
        if (XPManager.Instance == null) return;

        float newFill = XPManager.Instance.RemainingXPPercentage;

        if (newFill < _xpBarForegroundImage.fillAmount)
        {
            targetFill = 1f;
            isFilling = true;

            StartCoroutine(FillNextLevel(newFill));
        }
        else
        {
            targetFill = newFill;
            isFilling = true;
        }
    }

    private System.Collections.IEnumerator FillNextLevel(float leftoverFill)
    {
        while (_xpBarForegroundImage.fillAmount < 1f)
        {
            yield return null;
        }

        _xpBarForegroundImage.fillAmount = 0f;

        targetFill = leftoverFill;
        isFilling = true;
    }
}
