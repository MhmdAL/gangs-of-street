using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private List<PathDebug> paths;
    [SerializeField] private MinMaxCurve carSpeed;
    [SerializeField] private MinMaxCurve spawnInterval;

    private float _currentSpawnInterval;
    private float _spawnTimer;

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _currentSpawnInterval)
        {
            _spawnTimer -= _currentSpawnInterval;
            _currentSpawnInterval = Random.Range(spawnInterval.constantMin, spawnInterval.constantMax);

            SpawnCar();
        }
    }

    private void SpawnCar()
    {
        var pathIndex = Random.Range(0, paths.Count);
        var path = paths[pathIndex];

        var car = Instantiate(carPrefab, path.transform.GetChild(0).position, Quaternion.identity);

        var followPath = car.GetComponent<FollowPath>();

        followPath.path = path.gameObject;
        followPath.speed = Random.Range(carSpeed.constantMin, carSpeed.constantMax);
    }
}