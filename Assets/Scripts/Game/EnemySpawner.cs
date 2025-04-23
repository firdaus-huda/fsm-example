using System;
using System.Collections.Generic;
using NinjaFSM.Game.Character;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NinjaFSM.Game
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private int maxEnemyInGame;
        [SerializeField] private int spawnDelay;
        [SerializeField] private EnemyController enemyPrefab;

        [SerializeField] private Vector2 minPosition;
        [SerializeField] private Vector2 maxPosition;

        private readonly List<EnemyController> _enemies = new();
        private int _totalEnemyKilled;
        
        public static event Action<int> EnemyKilled;

        private void Start()
        {
            InvokeRepeating(nameof(Spawn), 0f, spawnDelay);
            EnemyKilled?.Invoke(_totalEnemyKilled);
        }

        private void Spawn()
        {
            if (_enemies.Count >= maxEnemyInGame) return;

            var spawnPosition = new Vector2(Random.Range(minPosition.x, maxPosition.x),
                Random.Range(minPosition.y, maxPosition.y));
            var enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
            enemy.Killed += OnEnemyKilled;
            _enemies.Add(enemy);
        }

        private void OnEnemyKilled(EnemyController enemy)
        {
            enemy.Killed -= OnEnemyKilled;
            _enemies.Remove(enemy);
            _totalEnemyKilled++;
            EnemyKilled?.Invoke(_totalEnemyKilled);
        }
    }
}