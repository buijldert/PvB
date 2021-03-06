﻿using System.Collections.Generic;
using UnityEngine;
using RR.Controllers;
using RR.Handlers;
using RR.Components;

namespace RR.Managers
{
    /// <summary>
    /// This class is responsible for managing the various chunks in the game, including but not limited to buildings and roads).
    /// </summary>
    public class ChunkManager : MonoBehaviour
    {
        private enum ChunkManagerSide
        {
            Left,
            Middle,
            Right
        }

        private Dictionary<string, Vector3> sizeDatabase = new Dictionary<string, Vector3>();

        [SerializeField] private GameObject[] chunkPrefabs;
        [SerializeField] private float movementSpeed = 60f;
        [SerializeField] private ChunkManagerSide chunkSide;

        private List<GameObject> chunkClones = new List<GameObject>();
        private float outOfScreenPosZ = 300f;
        private bool canMove;

        /// <summary>
        /// Start() is called before OnEnable() and Awake().
        /// </summary>
        private void Start()
        {
            FillSizeDatabase();
            for (int i = 0; i < 10f; i++)
            {
                chunkClones.Add(GetRandomChunk(Vector3.zero));
            }

            chunkClones[0].transform.position = new Vector3(0, 0, transform.position.z + sizeDatabase[chunkClones[0].name].z);
            SortChunks();
        }

        /// <summary>
        /// OnEnable() is called before Start() and after Awake().
        /// </summary>
        private void OnEnable()
        {
            UpdateMovementSpeed();
            CollisionHandler.OnDeadlyCollision += StopMovement;
            GameController.OnStartGame += StartMovement;
            GameController.OnStopGame += StopMovement;
            DifficultyManager.OnChangeDifficulty += UpdateMovementSpeed;
        }

        /// <summary>
        /// OnDisable() is called before the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= StopMovement;
            GameController.OnStartGame -= StartMovement;
            GameController.OnStopGame -= StopMovement;
            DifficultyManager.OnChangeDifficulty -= UpdateMovementSpeed;
        }

        /// <summary>
        /// Update() is called once per frame.
        /// </summary>
        private void Update()
        {
            if (canMove)
            {
                MoveAllChunks();
            }
        }

        /// <summary>
        /// Updates the movement speed to conform with the global.
        /// </summary>
        private void UpdateMovementSpeed()
        {
            movementSpeed = DifficultyManager.GLOBAL_MOVEMENT_SPEED;
        }

        /// <summary>
        /// Moves all chunks while the game is running.
        /// </summary>
        private void MoveAllChunks()
        {
            for (int i = 0; i < chunkClones.Count; i++)
            {
                MoveChunk(chunkClones[i], movementSpeed);
                if (chunkClones[chunkClones.Count - 1].transform.position.z < outOfScreenPosZ)
                {
                    chunkClones.Add(GetRandomChunk(Vector3.zero));
                    SortChunks();
                }

                if (ChunkOutOfBounds(chunkClones[i]))
                {
                    ObjectPool.instance.PoolObject(chunkClones[i]);
                    chunkClones.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Starts the movement of the chunks.
        /// </summary>
        private void StartMovement()
        {
            canMove = true;
        }

        /// <summary>
        /// Stops the movement of the chunks.
        /// </summary>
        private void StopMovement()
        {
            canMove = false;
        }

        /// <summary>
        /// Fills the chunk size database based on the chunkprefabs.
        /// </summary>
        private void FillSizeDatabase()
        {
            for (int i = 0; i < chunkPrefabs.Length; i++)
            {
                Vector3 size = chunkPrefabs[i].GetComponent<MeshRenderer>().bounds.extents * 2f;
                
                sizeDatabase.Add(chunkPrefabs[i].name, size);
            }
        }

        /// <summary>
        /// Spawns a random chunk and puts it at the given position.
        /// </summary>
        /// <param name="_position">The position at which the random chunk will be placed.</param>
        /// <returns>Returns the chunk that spawned.</returns>
        private GameObject GetRandomChunk(Vector3 _position)
        {
            return SpawnChunk(chunkPrefabs[Random.Range(0, chunkPrefabs.Length)], _position);
        }

        /// <summary>
        /// Spawns the given chunk prefab at the given position and rotation based on if its the left or right manager.
        /// </summary>
        /// <param name="_chunk">The chunk prefab that will be spawned.</param>
        /// <param name="_position"></param>
        /// <returns></returns>
        private GameObject SpawnChunk(GameObject _chunk, Vector3 _position)
        {
            GameObject chunkClone = ObjectPool.instance.GetObjectForType(_chunk.name, false);
            chunkClone.transform.position = _position;
            chunkClone.transform.SetParent(transform);
            if (chunkSide == ChunkManagerSide.Left)
            {
                chunkClone.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if(chunkSide == ChunkManagerSide.Right)
            {
                chunkClone.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            return chunkClone;
        }

        /// <summary>
        /// Sorts the chunks behind each other.
        /// </summary>
        private void SortChunks()
        {
            if (chunkClones.Count < 1)
            {
                Debug.Log("No chunks to sort.");
                return;
            }

            Vector3 previousChunkPos = chunkClones[0].transform.position;
            for (int i = 0; i < chunkClones.Count; i++)
            {
                if (i > 0)
                {
                    previousChunkPos.z += (sizeDatabase[chunkClones[i].name].z * .5f);
                }
                chunkClones[i].transform.position = new Vector3(transform.position.x, 0f, previousChunkPos.z);
                previousChunkPos.z += (sizeDatabase[chunkClones[i].name].z * .5f);
            }
        }

        /// <summary>
        /// Moves the given chunk at the given speed.
        /// </summary>
        /// <param name="_chunk">The chunk to move.</param>
        /// <param name="_speed">The speed at which the chunk will be moved.</param>
        private void MoveChunk(GameObject _chunk, float _speed)
        {
            _chunk.transform.position -= new Vector3(0, 0, _speed * Time.deltaTime);
        }

        /// <summary>
        /// Checks if the chunk is out of bounds.
        /// </summary>
        /// <param name="_chunk">The chunk that will be checked.</param>
        /// <returns>A boolean signalling whether the chunk is out of bounds or not.</returns>
        private bool ChunkOutOfBounds(GameObject _chunk)
        {
            return _chunk.transform.position.z < transform.position.z;
        }
    }
}