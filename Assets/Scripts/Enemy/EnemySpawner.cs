using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;

        [SerializeField] private Transform[] _spawnPoints;

        [SerializeField] float _spawnRate;

        private float _timer;

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > _spawnRate)
            {
                Spawn();
                _timer = 0;
            }
        }

        private void Spawn()
        {
            Vector2 spawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
            Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

