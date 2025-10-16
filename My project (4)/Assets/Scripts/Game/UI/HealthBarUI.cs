using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image _healthBarForegroundImage;

    public void UpdateHealthBar(PlayerHealth PlayerHealth)
    {
        _healthBarForegroundImage.fillAmount = PlayerHealth.RemainingHealthPercentage;
    }
}
