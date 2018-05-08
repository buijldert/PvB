using SynchronizerData;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Utility;

namespace Environment
{
    public class ObstacleGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject[] obstaclePrefabs;
        private List<GameObject> obstacleClones = new List<GameObject>();

        private float spawnDelay = .75f;
        private float backPosZ = 200f;

        private Coroutine spawningCoroutine;

        private float[] xOffsets = new float[2] { -3f, 3f };

        public Vector3[] beatPositions;

        private BeatObserver beatObserver;
        private int beatCounter;

        private int counter;

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
            beatObserver = GetComponent<BeatObserver>();
            beatCounter = 0;
        }

        void Update()
        {
            if ((beatObserver.beatMask & BeatType.OnBeat) == BeatType.OnBeat)
            {
                beatCounter = (++beatCounter == beatPositions.Length ? 0 : beatCounter);
                counter++;
                //if (counter % 2 == 0)
                //{
                    OnOnbeatDetected();
                //}

            }

        }

        public void OnOnbeatDetected()
        {
            Debug.Log("beat");
            SpawnObstacle();
        }

        /// <summary>
        /// Engages the spawning of obstacles.
        /// </summary>
        private void StartSpawning()
        {
            spawningCoroutine = StartCoroutine(SpawningCoroutine());
        }

        /// <summary>
        /// Constantly spawns new obstacles on both lanes.
        /// </summary>
        private IEnumerator SpawningCoroutine()
        {
            spawnDelay = Random.Range(0.5f, 0.75f);
            yield return new WaitForSeconds(spawnDelay);
            SpawnObstacle();
            spawningCoroutine = StartCoroutine(SpawningCoroutine());
        }

        /// <summary>
        /// Stops the spawning when 
        /// </summary>
        private void StopSpawning()
        {
            if(spawningCoroutine != null)
                StopCoroutine(spawningCoroutine);

            for (int i = 0; i < obstacleClones.Count; i++)
            {
                ObjectPool.Instance.PoolObject(obstacleClones[i]);
            }
            obstacleClones.Clear();
        }

        private void SpawnObstacle()
        {
            int randomObstacle = Random.Range(0, 2);
            GameObject obstacleClone = ObjectPool.Instance.GetObjectForType(obstaclePrefabs[randomObstacle].name, false);
            obstacleClone.transform.position = new Vector3(xOffsets[Random.Range(0, 2)], transform.position.y, 200f);
            obstacleClone.transform.SetParent(transform);
            obstacleClones.Add(obstacleClone);
        }
    }
}