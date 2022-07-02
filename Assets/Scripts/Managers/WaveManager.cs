using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;
using UnityEngine.AI;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject _spawnerLocations;
    private List<PoolableObjectData> _enemyList = new List<PoolableObjectData>();
    [SerializeField] private int _currentWave;
    [SerializeField] private int _waveValue;
    [SerializeField] private int _timeBetweenWaves;
    private List<PoolableObjectData> _generatedWave = new List<PoolableObjectData>();
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private float _playerSpawnRange;
    [SerializeField] private List<GameObject> _spawnedEnemies;

    private List<SpawnPointData> _spawnerPoints = new List<SpawnPointData>();
    private int _waveDuration;
    private float _waveTimer;
    private float _spawnInterval;
    private float _spawnTimer;
    private float _betweenTimer;

    private EventManager _eventManager;

    [SerializeField] private WaveManagerStates _state;

    private void Awake() {
        _playerManager = FindObjectOfType<PlayerManager>();
    }

    private void OnEnable() {
        EventManager.Instance.onPlay += OnPlay;
        EventManager.Instance.onSkip += OnSkip;

    }

    private void OnDisable() {
        EventManager.Instance.onPlay -= OnPlay;
         EventManager.Instance.onSkip -= OnSkip;
    }

    private void OnPlay() {
        ProocedToNextWave();
    }

    private void OnSkip() {
        ProocedToNextWave();
    }

    void Start()
    {
        _enemyList = ObjectPoolingManager.Instance.PoolableObjectList;
        SetupSpawnerList();
    }

    private void SetupSpawnerList() 
    {   
        Vector3 position;
        float distance;
        foreach (Transform transform in _spawnerLocations.GetComponentInChildren<Transform>()) {
            position = transform.position;
            distance = Vector3.Distance(position, this.transform.position);
            _spawnerPoints.Add(new SpawnPointData(position, distance));
        }
    }

    void Update()
    {
        switch (_state) {
            case WaveManagerStates.InWave:
                if (_spawnTimer <= 0) {
                if (_generatedWave.Count > 0) {
                    GameObject obj = SpawnObjectWithPooling(_generatedWave[0], GetSpawnPosition());
                    _generatedWave.RemoveAt(0);
                    _spawnTimer = _spawnInterval;

                    _spawnedEnemies.Add(obj);
                }
                else {
                    _waveTimer = 0;
                }   
                }
                else {
                _spawnTimer -= Time.deltaTime;
                _waveTimer -= Time.deltaTime;
                }
                
                // There are no remeaning spawned enemies.
                if (_spawnedEnemies.Count <= 0) {
                    EndWave();
                }
        break;

        case WaveManagerStates.BetweenWave:
            if (_betweenTimer <= 0) {
                ProocedToNextWave();
            }
            else {
                _betweenTimer -= Time.deltaTime;
                EventManager.Instance.onUpdateTimer?.Invoke((int)_betweenTimer);
            }
        break;
        }
    }

    private void EndWave() {
        EventManager.Instance.onWaveEnd?.Invoke();
        _state = WaveManagerStates.BetweenWave;
    }

    public void ProocedToNextWave() {
        _state = WaveManagerStates.Idle;
        _betweenTimer = _timeBetweenWaves;
        _currentWave++;
        GenerateWave();
    }

    private void GenerateWave() {
        _waveValue = _currentWave * 10;
        GenerateEnemies();

        _spawnInterval = _waveDuration / _generatedWave.Count;
        _waveTimer = _waveDuration;
    }

    private void GenerateEnemies() {
        List<PoolableObjectData> generatedEnemies = new List<PoolableObjectData>();
        while (_waveValue > 0) {
            int randomEnemyID = Random.Range(0, _enemyList.Count);
            int randomEnemyCost = _enemyList[randomEnemyID].Cost;

            if (_waveValue - randomEnemyCost >= 0) {
                generatedEnemies.Add(_enemyList[randomEnemyID]);
                _waveValue -= randomEnemyCost;
            }
            else if (_waveValue <= 0) {
                break;
            }
        }
        _generatedWave.Clear();
        _generatedWave = generatedEnemies;

        EventManager.Instance.onWaveStart?.Invoke(_currentWave);
        _state = WaveManagerStates.InWave;
    }

    private Vector3 GetSpawnPosition() {
        float distance = Vector3.Distance(transform.position, _playerManager.transform.position);
        List<Vector3> availablePoints = new List<Vector3>();
        foreach (SpawnPointData point in _spawnerPoints) {
            // REDO THIS CALCULATION
            if (point.Distance - distance <= _playerSpawnRange) {
                availablePoints.Add(point.Position);
            }
        }
        int randomPointIndex = Random.Range(0, availablePoints.Count);
        return availablePoints[randomPointIndex];
    }

    public GameObject SpawnObjectWithPooling(PoolableObjectData data, Vector3 position)
    {
        var spawnedObj = ObjectPoolingManager.Instance.DequeuePoolableGameObject(data);

        if (spawnedObj.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent)) {
            agent.Warp(position);
        }
        else {
            spawnedObj.transform.position = position;
        }
        spawnedObj.GetComponent<PoolableObjectController>().IsCalledByPooling = true;
        return spawnedObj;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if (_playerManager != null) Gizmos.DrawWireSphere(_playerManager.transform.position, _playerSpawnRange);
    }

    public void RemoveFromSpawnedList(GameObject obj) {
        _spawnedEnemies.Remove(obj);
    }
}
