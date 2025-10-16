using UnityEngine;
using UnityEngine.Events;

public class XPManager : MonoBehaviour
{
    public static XPManager Instance { get; private set; }

    [SerializeField] private int currentXP = 0;
    [SerializeField] private int xpToLevel = 10;
    [SerializeField] private float xpMultiplier = 1.5f;

    public int CurrentLevel { get; private set; } = 1;

    public UnityEvent OnXPChanged;
    public UnityEvent OnLevelUp;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public float RemainingXPPercentage => Mathf.Clamp01((float)currentXP / xpToLevel);

    public void AddXP(int amount)
    {
        currentXP += amount;

        
        while (currentXP >= xpToLevel)
        {
            currentXP -= xpToLevel;
            LevelUp();
        }

        OnXPChanged?.Invoke();
    }

    private void LevelUp()
    {
        CurrentLevel++;
        xpToLevel = Mathf.CeilToInt(xpToLevel * xpMultiplier);

        Debug.Log($"LEVEL UP! Now level {CurrentLevel}, next requires {xpToLevel} XP.");

        OnLevelUp?.Invoke();
        OnXPChanged?.Invoke(); 

        
        UpgradeManager upgradeManager = FindObjectOfType<UpgradeManager>();
        if (upgradeManager != null)
        {
            upgradeManager.OpenUpgradeMenu();
        }
    }
}



