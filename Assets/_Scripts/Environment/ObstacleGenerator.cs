using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Utility;

namespace Environment
{
    public class ObstacleGenerator : MonoBehaviour, IAudioCallback
    {
        [SerializeField] private GameObject[] _obstaclePrefabs;
        private List<GameObject> _obstacleClones = new List<GameObject>();

        private float _spawnDelay = .75f;
        private float _backPosZ = 200f;

        private Coroutine _spawningCoroutine;

        private float[] _xOffsets = new float[2] { -3f, 3f };

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

        private void Start()
        {
            AudioProcessor processor = FindObjectOfType<AudioProcessor>();
            processor.AddAudioCallback(this);
        }

        public void onOnbeatDetected()
        {
            Debug.Log("beat");
            SpawnObstacle();
        }

        public void onSpectrum(float[] spectrum)
        {

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
            SpawnObstacle();
            _spawningCoroutine = StartCoroutine(SpawningCoroutine());
        }

        /// <summary>
        /// Stops the spawning when 
        /// </summary>
        private void StopSpawning()
        {
            if(_spawningCoroutine != null)
                StopCoroutine(_spawningCoroutine);

            for (int i = 0; i < _obstacleClones.Count; i++)
            {
                ObjectPool.Instance.PoolObject(_obstacleClones[i]);
            }
            _obstacleClones.Clear();
        }

        private void SpawnObstacle()
        {
            int randomObstacle = Random.Range(0, 2);
            GameObject obstacleClone = ObjectPool.Instance.GetObjectForType(_obstaclePrefabs[randomObstacle].name, false);
            obstacleClone.transform.position = new Vector3(_xOffsets[Random.Range(0, 2)], transform.position.y, 200f);
            obstacleClone.transform.SetParent(transform);
            _obstacleClones.Add(obstacleClone);
        }
    }
}