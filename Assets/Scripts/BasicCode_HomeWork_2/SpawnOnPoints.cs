using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnPoints : MonoBehaviour
{
    [SerializeField] private GameObject _template;
    [SerializeField] private float _heightSpawn;
    [SerializeField] private float _timeSpawn;

    private Transform[] _spawnPoints;
    private int _currentPoint;
    private float _currentTime;

    private void Start()
    {
        _spawnPoints = new Transform[transform.childCount];

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i] = transform.GetChild(i);
        }
    }

    private void Update()
    {
        if (_currentTime == 0)
        {
            Instantiate(_template, new Vector2(_spawnPoints[_currentPoint].position.x, _spawnPoints[_currentPoint].position.y + _heightSpawn), Quaternion.identity);

            if (_currentPoint == _spawnPoints.Length - 1)
            {
                _currentPoint = 0;
            } 
            else 
                _currentPoint++;
        }

        _currentTime += Time.deltaTime;

        if (_currentTime >= _timeSpawn) 
            _currentTime = 0;
    }
}
