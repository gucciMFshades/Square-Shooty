using UnityEngine;

public class Enemy1Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _minimumSpawnTime = 2f;
    [SerializeField] private float _maximumSpawnTime = 2f;
    [SerializeField] private float _spawnAccelerationInterval = 10f; 
    [SerializeField] private float _spawnRateMultiplier = 0.9f;      
    [SerializeField] private float _minimumLimit = 0.5f;             

    private float _timeUntilSpawn;
    private float _gameTimer;
    private bool _isPaused = false;

    private void Awake()
    {
        ResetSpawner();
    }

    private void Update()
    {
        if (_isPaused) return;

        _gameTimer += Time.deltaTime;
        _timeUntilSpawn -= Time.deltaTime;

        if (_timeUntilSpawn <= 0)
        {
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            SetTimeUntilSpawn();
        }

        
        if (_gameTimer >= _spawnAccelerationInterval)
        {
            _gameTimer = 0f;
            AccelerateSpawnRate();
        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }

    private void AccelerateSpawnRate()
    {
        _minimumSpawnTime = Mathf.Max(_minimumSpawnTime * _spawnRateMultiplier, _minimumLimit);
        _maximumSpawnTime = Mathf.Max(_maximumSpawnTime * _spawnRateMultiplier, _minimumLimit + 0.25f);
    }

    public void PauseSpawning(bool pause)
    {
        _isPaused = pause;
    }

    public void ResetSpawner()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
        _gameTimer = 0f;
        _isPaused = false;
    }
}
