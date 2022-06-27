using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnPoints : MonoBehaviour
{
    [SerializeField] private EnemySlime _template;
    [SerializeField] private float _heightSpawn;
    [SerializeField] private float _timeSpawn;

    private Transform[] _spawnPoints;
    private int _currentPoint;
    private bool _isSpawning = true;

    private void Start()
    {
        _spawnPoints = new Transform[transform.childCount];

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i] = transform.GetChild(i);
        }

        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        var waitTimeSpawn = new WaitForSeconds(_timeSpawn);

        while (_isSpawning)
        {
            Instantiate(_template, new Vector2(_spawnPoints[_currentPoint].position.x, _spawnPoints[_currentPoint].position.y + _heightSpawn), Quaternion.identity);

            if (_currentPoint == _spawnPoints.Length - 1)
            {
                _currentPoint = 0;
            }
            else
                _currentPoint++;

            yield return waitTimeSpawn;
        }
    }
}
