using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    [SerializeField]
    private SoundController _soundController;

    [SerializeField]
    private Transform[] spawns;

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _bossPrefab;

    public float EnemyMovementSpeed { get ; set; } = 2f;
    public float MovementSpeedMultiplier { get; } = 1.15f;
    public float EnemyHealth { get; set; } = 2f;

    public static float TimeBetweenWaves = 10f;
    private float _waveMultiplier = 2.25f;

    private float _spawnInterval;

    private int _enemiesAtLevelOne = 7;

	private int _enemiesToSpawn;
    private int _enemiesSpawned;

    private int _enemiesRemaining;
    public int EnemiesRemaining {
        get {
            return _enemiesRemaining;
        }
        set {
            if (_enemiesRemaining == value)
                return;
                
            _enemiesRemaining = value;
            EventManager.CallKillEnemy();
            if (_enemiesRemaining <= 0 && _enemiesSpawned == _enemiesToSpawn) {
                EventManager.CallCompleteWave();
            }
        }
    }
    
    public static int Wave { get; private set; }

    private void OnEnable() {
        EventManager.OnWaveCompleted += CompleteWave;
    }

    private void OnDisable() {
        EventManager.OnWaveCompleted -= CompleteWave;
    }

    private void Awake() {
        _spawnInterval = 0.75f;
        _enemiesToSpawn = _enemiesAtLevelOne;
        _enemiesRemaining = _enemiesToSpawn;
        Wave = 1;

        StartCoroutine(BeginWave());
        /*for(int i = 1; i < 51; i++) {
            float enemyCount = 0;
            if(i == 1)
                enemyCount = _enemiesAtLevelOne;
            else
                enemyCount = _enemiesToSpawn * Mathf.Pow(1.045f, _waveMultiplier);
            _enemiesToSpawn = Mathf.RoundToInt(enemyCount);
            Debug.Log("Wave " + i + ": " + _enemiesToSpawn);
        }*/
    }

    public void CompleteWave() {
        Enemy enemy = _enemyPrefab.GetComponent<Enemy>();
        Wave++;
        if (Wave % 5 == 0)
            EnemyMovementSpeed *= MovementSpeedMultiplier;
        if (Wave % 5 == 0) {
            if (_spawnInterval >= 0.1f)
                _spawnInterval -= 0.05f;
        }

        _enemiesSpawned = 0;
        EnemiesRemaining = 0;
        float enemyCount = _enemiesToSpawn * Mathf.Pow(1.045f, _waveMultiplier);
        _enemiesToSpawn = Mathf.RoundToInt(enemyCount);
        StartCoroutine(BeginWave());
    }

    private IEnumerator BeginWave() {
        if (Wave > 1) {
            yield return new WaitForSeconds(TimeBetweenWaves);
        }
        EnemiesRemaining = _enemiesToSpawn;
        bool spawnBoss = false;
        bool bossSpawned = false;
        int random = Random.Range(1, _enemiesToSpawn);
        if (Random.Range(1, 6) == 1) {
            spawnBoss = true;
            _soundController.PlayBossSound();
        }
        while(_enemiesSpawned < _enemiesToSpawn) {
            if (spawnBoss && !bossSpawned && random == _enemiesSpawned) {
                Instantiate(_bossPrefab, GetRandomSpawn(), Quaternion.identity).GetComponent<Enemy>();
                
                bossSpawned = true;
            } else {
                Instantiate(_enemyPrefab, GetRandomSpawn(), Quaternion.identity);
            }
            _enemiesSpawned++;
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    public bool IsWaveOver() {
        return _enemiesSpawned == _enemiesToSpawn && gameObject.transform.childCount <= 1;
    }

    public Vector2 GetRandomSpawn() {
        Vector2 topLeftToRight = new Vector2(Random.Range(spawns[0].position.x, spawns[1].position.x), spawns[0].position.y);
        Vector2 bottomLeftToRight = new Vector2(Random.Range(spawns[2].position.x, spawns[3].position.x), spawns[2].position.y);
        Vector2 leftTopToBottom = new Vector2(spawns[0].position.x, Random.Range(spawns[0].position.y, spawns[2].position.y));
        Vector2 rightTopToBottom = new Vector2(spawns[1].position.x, Random.Range(spawns[1].position.y, spawns[3].position.y));

        Vector2[] possibleSpawns = { topLeftToRight, bottomLeftToRight, leftTopToBottom, rightTopToBottom };
        return possibleSpawns[Random.Range(0, possibleSpawns.Length)];
    }
}
