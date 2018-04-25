using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Utility;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _chunkPrefabs;
    private List<GameObject> _chunkClones = new List<GameObject>();

    [SerializeField] private float _movementSpeed = 30f;
    private bool _canmove;

    private void Start ()
    {
        for (int i = 0; i < 20; i++)
        {
            _chunkClones.Add(GetRandomChunk(Vector3.zero));
        }

        _chunkClones[0].transform.position = new Vector3(0, 0, 0f + GetSize(_chunkClones[0]).z);
        SortChunks();
	}

    private void OnEnable()
    {
        CollisionHandler.OnDeadlyCollision += StopMovement;
        RestartGameButton.OnRestartGame += StartMovement;
    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
        if (_canmove)
        {
            for (int i = 0; i < _chunkClones.Count; i++)
            {
                MoveChunk(_chunkClones[i], _movementSpeed);
                if (_chunkClones[_chunkClones.Count - 1].transform.position.z < 350f)
                {
                    _chunkClones.Add(GetRandomChunk(Vector3.zero));
                }
                SortChunks();
                if (ChunkOutOfBounds(_chunkClones[i]))
                {
                    ObjectPool.Instance.PoolObject(_chunkClones[i]);
                    _chunkClones.RemoveAt(i);
                }
            }
        }
    }

    private void StartMovement()
    {
        _canmove = true;
    }

    private void StopMovement()
    {
        _canmove = false;
    }

    /// <summary>
    /// Gets the size of the given chunk.
    /// </summary>
    /// <param name="chunk">The chunk of which the size will be retuned.</param>
    /// <returns>The size of the given chunk.</returns>
    private Vector3 GetSize(GameObject chunk)
    {
        Vector3 size = chunk.GetComponent<MeshRenderer>().bounds.extents * 2f;
        return size;
    }

    /// <summary>
    /// Spawns a random chunk and puts it at the given position.
    /// </summary>
    /// <param name="_position">The position at which the random chunk will be placed.</param>
    /// <returns>Returns the chunk that spawned.</returns>
    private GameObject GetRandomChunk(Vector3 _position)
    {
        return SpawnChunk(_chunkPrefabs[Random.Range(0, _chunkPrefabs.Length)], _position);
    }

    /// <summary>
    /// Spawns the given chunk prefab at the given position.
    /// </summary>
    /// <param name="chunk">The chunk prefab that will be spawned.</param>
    /// <param name="position"></param>
    /// <returns></returns>
    private GameObject SpawnChunk(GameObject chunk, Vector3 position)
    {
        GameObject chunkClone = ObjectPool.Instance.GetObjectForType(chunk.name, false);
        chunkClone.transform.position = position;
        chunkClone.transform.SetParent(transform);
        return chunkClone;
    }

    /// <summary>
    /// Sorts the chunks behind each other.
    /// </summary>
    private void SortChunks()
    {
        if (_chunkClones.Count < 1)
        {
            Debug.Log("No chunks to sort.");
            return;
        }

        Vector3 previousChunkPos = _chunkClones[0].transform.position;
        for (int i = 0; i < _chunkClones.Count; i++)
        {
            if(i > 0)
                previousChunkPos.z += (GetSize(_chunkClones[i]).z / 2f);
            _chunkClones[i].transform.position = new Vector3(transform.position.x, GetSize(_chunkClones[i]).y / 2f, previousChunkPos.z);
            previousChunkPos.z += (GetSize(_chunkClones[i]).z / 2f);
        }
    }

    /// <summary>
    /// Moves the given chunk at the given speed.
    /// </summary>
    /// <param name="chunk">The chunk to move.</param>
    /// <param name="speed">The speed at which the chunk will be moved.</param>
    private void MoveChunk(GameObject chunk, float speed)
    {
        chunk.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
    }

    /// <summary>
    /// Checks if the chunk is out of bounds.
    /// </summary>
    /// <param name="chunk">The chunk that will be checked.</param>
    /// <returns>A boolean signalling whether the chunk is out of bounds or not.</returns>
    private bool ChunkOutOfBounds(GameObject chunk)
    {
        if (chunk.transform.position.z < 0)
            return true;
        else
            return false;
    }
}
