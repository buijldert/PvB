using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Utility;

namespace Environment
{
    public class ObstacleGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject[] _obstaclePrefabs;

        private float _spawnDelay = .75f;

        private Coroutine _spawningCoroutine;

        private float[] _xOffsets = new float[2] { -12.5f, 12.5f };

        private void OnEnable()
        {
            CollisionHandler.OnDeadlyCollision += StopSpawning;
            RestartGameButton.OnRestartGame += StartSpawning;
        }

        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= StopSpawning;
            RestartGameButton.OnRestartGame -= StartSpawning;
        }

        /// <summary>
        /// Engages the spawning of obstacles.
        /// </summary>
        private void StartSpawning()
        {
            _spawningCoroutine = StartCoroutine(SpawningCoroutine());
        }

        /// <summary>
        /// Constantly spawns new obstacles on both lanes.
        /// </summary>
        private IEnumerator SpawningCoroutine()
        {
            _spawnDelay = Random.Range(0.5f, 0.75f);
            yield return new WaitForSeconds(_spawnDelay);
            int randomObstacle = Random.Range(0, 2);
            GameObject obstacleClone = ObjectPool.Instance.GetObjectForType(_obstaclePrefabs[randomObstacle].name, false);
            obstacleClone.transform.position = new Vector3(_xOffsets[Random.Range(0, 2)], transform.position.y, 350f);
            obstacleClone.transform.SetParent(transform);
            _spawningCoroutine = StartCoroutine(SpawningCoroutine());
        }

        /// <summary>
        /// Stops the spawning when 
        /// </summary>
        private void StopSpawning()
        {
            if(_spawningCoroutine != null)
                StopCoroutine(_spawningCoroutine);

            for (int i = 0; i < transform.childCount; i++)
            {
                ObjectPool.Instance.PoolObject(transform.GetChild(i).gameObject);
            }
        }
    }
}