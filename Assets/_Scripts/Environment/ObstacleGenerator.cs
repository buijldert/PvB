using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Environment
{
    /// <summary>
    /// This class is responsible for spawning obstacles when the signal(beat) is given.
    /// </summary>
    public class ObstacleGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject[] obstaclePrefabs;
        private List<GameObject> obstacleClones = new List<GameObject>();

        private Coroutine spawningCoroutine;

        private float backPosZ = 400f;
        private float[] xOffsets = new float[2] { -3f, 3f };
        private float timeTillChanceIncrease = 20f;

        private BeatObserver beatObserver;

        //private int counter;

        private int[] lastTwoLanes = new int[2];
        private int chanceToSpawnDoubleGates;

        private Coroutine doubleGateChanceCoroutine;

        private void OnEnable()
        {
            CollisionHandler.OnDeadlyCollision += StopSpawning;
            PoolOverTime.OnObstacleCollection += RemoveObstacleFromList;
            GameController.OnStartGame += StartSpawning;
        }

        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= StopSpawning;
            PoolOverTime.OnObstacleCollection -= RemoveObstacleFromList;
            GameController.OnStartGame -= StartSpawning;
        }

        private void Start()
        {
            for (int i = 0; i < lastTwoLanes.Length; i++)
            {
                lastTwoLanes[i] = 5;
            }

            beatObserver = GetComponent<BeatObserver>();
        }

        private void Update()
        {
            if ((beatObserver.beatMask & BeatType.OnBeat) == BeatType.OnBeat)
            {
                //counter++;
                OnOnbeatDetected();
            }

        }

        private void StartSpawning()
        {
            chanceToSpawnDoubleGates = 0;
            doubleGateChanceCoroutine = StartCoroutine(ChanceForDoubleGatesIncreases());
        }

        private IEnumerator ChanceForDoubleGatesIncreases()
        {
            yield return new WaitForSeconds(timeTillChanceIncrease);
            chanceToSpawnDoubleGates += 1;
            if(chanceToSpawnDoubleGates < 20)
            {
                doubleGateChanceCoroutine = StartCoroutine(ChanceForDoubleGatesIncreases());
            }
        }

        private void RemoveObstacleFromList(GameObject obstacleToCollect)
        {
            obstacleClones.Remove(obstacleToCollect);
        }

        /// <summary>
        /// Spawns an obstacle once a beat is detected.
        /// </summary>
        public void OnOnbeatDetected()
        {
            SpawnObstacle();
        }

        /// <summary>
        /// Stops the spawning when the player is game over.
        /// </summary>
        private void StopSpawning()
        {
            for (int i = 0; i < obstacleClones.Count; i++)
            {
                ObjectPool.Instance.PoolObject(obstacleClones[i]);
            }
            obstacleClones.Clear();
            if(doubleGateChanceCoroutine != null)
            {
                StopCoroutine(doubleGateChanceCoroutine);
            }
        }

        /// <summary>
        /// Spawns a random obstacle in a random lane.
        /// </summary>
        private void SpawnObstacle()
        {
            int randomObstacle = Random.Range(0, 2);
            int randomChance = Random.Range(0, 100);
            if(randomChance < chanceToSpawnDoubleGates)
            {
                GameObject leftGate = ObjectPool.Instance.GetObjectForType(obstaclePrefabs[randomObstacle].name, false);
                GameObject rightGate = ObjectPool.Instance.GetObjectForType(obstaclePrefabs[randomObstacle].name, false);
                leftGate.transform.position = new Vector3(xOffsets[0], transform.position.y, backPosZ);
                rightGate.transform.position = new Vector3(xOffsets[1], transform.position.y, backPosZ);
                obstacleClones.Add(leftGate);
                obstacleClones.Add(rightGate);
            }
            else
            {

                GameObject obstacleClone = ObjectPool.Instance.GetObjectForType(obstaclePrefabs[randomObstacle].name, false);
                obstacleClone.transform.position = new Vector3(xOffsets[MakeRandomCheck()], transform.position.y, backPosZ);
                obstacleClone.transform.SetParent(transform);
                obstacleClones.Add(obstacleClone);
            }
        }

        /// <summary>
        /// Checks if there have been too many gates on one side of the road in other words makes random feel a little more fair.
        /// </summary>
        /// <returns>Returns the index of the obstacle that will be spawned.</returns>
        private int MakeRandomCheck()
        {
            int randomLane = Random.Range(0, 2);
            if (randomLane == lastTwoLanes[0] && randomLane == lastTwoLanes[1])
            {
                if (randomLane == 0)
                {
                    randomLane = 1;
                }  
                else
                {
                    randomLane = 0;
                }
            }
            lastTwoLanes[1] = lastTwoLanes[0];
            lastTwoLanes[0] = randomLane;
            return randomLane;
        }
    }
}