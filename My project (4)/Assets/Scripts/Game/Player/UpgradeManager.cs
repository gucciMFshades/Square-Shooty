using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject upgradeMenu; 
    [SerializeField] private Transform buttonContainer; 
    [SerializeField] private GameObject upgradeButtonPrefab; 

    [Header("Player References")]
    private PlayerShoot playerShoot;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;

    
    private Enemy1Spawner enemySpawner;

    private List<UpgradeOption> allUpgrades = new List<UpgradeOption>();

    private void Start()
    {
        playerShoot = FindObjectOfType<PlayerShoot>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        enemySpawner = FindObjectOfType<Enemy1Spawner>(); 

        
        if (upgradeMenu != null)
            upgradeMenu.SetActive(false);

        
        allUpgrades = new List<UpgradeOption>
        {
            new UpgradeOption("Bullet Damage +1", () => playerShoot.damage += 1),
            new UpgradeOption("Fire Rate +10%", () => playerShoot.fireRate *= 0.9f),
            new UpgradeOption("Bullet Speed +20%", () => playerShoot.bulletSpeed *= 1.2f),
            new UpgradeOption("Move Speed +10%", () => playerMovement.speed *= 1.1f),
            new UpgradeOption("Max Health +20%", () => {
                playerHealth.MaxHealth = Mathf.RoundToInt(playerHealth.MaxHealth * 1.2f);
                playerHealth.AddHealth(0);
            })
        };
    }

    public void OpenUpgradeMenu()
    {
        Time.timeScale = 0f;
        upgradeMenu.SetActive(true);

        
        enemySpawner?.PauseSpawning(true);

        
        foreach (Transform child in buttonContainer)
            Destroy(child.gameObject);

        
        List<UpgradeOption> randomUpgrades = GetRandomUpgrades(3);

        foreach (var upgrade in randomUpgrades)
        {
            GameObject buttonObj = Instantiate(upgradeButtonPrefab, buttonContainer);
            TMP_Text buttonText = buttonObj.GetComponentInChildren<TMP_Text>();
            buttonText.text = upgrade.Name;

            Button button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(() => ChooseUpgrade(upgrade));
        }
    }

    private void ChooseUpgrade(UpgradeOption upgrade)
    {
        upgrade.Apply();
        CloseUpgradeMenu();
    }

    private void CloseUpgradeMenu()
    {
        Time.timeScale = 1f;
        upgradeMenu.SetActive(false);

        
        enemySpawner?.PauseSpawning(false);
    }

    private List<UpgradeOption> GetRandomUpgrades(int count)
    {
        List<UpgradeOption> copy = new List<UpgradeOption>(allUpgrades);
        List<UpgradeOption> result = new List<UpgradeOption>();

        for (int i = 0; i < count && copy.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, copy.Count);
            result.Add(copy[randomIndex]);
            copy.RemoveAt(randomIndex);
        }

        return result;
    }
}

public class UpgradeOption
{
    public string Name { get; }
    public System.Action Apply { get; }

    public UpgradeOption(string name, System.Action apply)
    {
        Name = name;
        Apply = apply;
    }
}

