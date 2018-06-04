﻿using Audio;
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

        private BeatObserver beatObserver;

        //private int counter;

        private int[] lastTwoLanes = new int[2];

        private void OnEnable()
        {
            CollisionHandler.OnDeadlyCollision += StopSpawning;
            PoolOverTime.OnObstacleCollection += RemoveObstacleFromList;
        }

        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= StopSpawning;
            PoolOverTime.OnObstacleCollection -= RemoveObstacleFromList;
        }

        private void Start()
        {
            for (int i = 0; i < lastTwoLanes.Length; i++)
            {
                lastTwoLanes[i] = 5;
            }

            beatObserver = GetComponent<BeatObserver>();
        }

        void Update()
        {
            if ((beatObserver.beatMask & BeatType.OnBeat) == BeatType.OnBeat)
            {
                //counter++;
                OnOnbeatDetected();
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
            //Debug.Log("beat");
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
        }

        /// <summary>
        /// Spawns a random obstacle in a random lane.
        /// </summary>
        private void SpawnObstacle()
        {
            int randomObstacle = Random.Range(0, 2);
            
            GameObject obstacleClone = ObjectPool.Instance.GetObjectForType(obstaclePrefabs[randomObstacle].name, false);
            obstacleClone.transform.position = new Vector3(xOffsets[MakeRandomCheck()], transform.position.y, backPosZ);
            obstacleClone.transform.SetParent(transform);
            obstacleClones.Add(obstacleClone);
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