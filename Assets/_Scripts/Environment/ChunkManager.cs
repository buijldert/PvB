using System.Collections.Generic;
using UI;
using UnityEngine;
using Utility;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private GameObject[] chunkPrefabs;
    private List<GameObject> chunkClones = new List<GameObject>();
    
    [SerializeField] private float movementSpeed = 30f;

    private bool canmove;

    private void Start ()
    {
        for (int i = 0; i < 20; i++)
        {
            chunkClones.Add(GetRandomChunk(Vector3.zero));
        }

        chunkClones[0].transform.position = new Vector3(0, 0, transform.position.z + GetSize(chunkClones[0]).z);
        SortChunks();
	}

    private void OnEnable()
    {
        CollisionHandler.OnDeadlyCollision += StopMovement;
        RestartGameButton.OnRestartGame += StartMovement;
    }

    private void OnDisable()
    {
        CollisionHandler.OnDeadlyCollision -= StopMovement;
        RestartGameButton.OnRestartGame -= StartMovement;
    }

    private void Update()
    {
        if (canmove)
        {
            MoveAllChunks();
        }
    }
    
    /// <summary>
    /// Moves all chunks while the game is running.
    /// </summary>
    private void MoveAllChunks()
    {
        for (int i = 0; i < chunkClones.Count; i++)
        {
            MoveChunk(chunkClones[i], movementSpeed);
            if (chunkClones[chunkClones.Count - 1].transform.position.z < 200f)
            {
                chunkClones.Add(GetRandomChunk(Vector3.zero));
            }
            SortChunks();
            if (ChunkOutOfBounds(chunkClones[i]))
            {
                ObjectPool.Instance.PoolObject(chunkClones[i]);
                chunkClones.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Starts the movement of the chunks.
    /// </summary>
    private void StartMovement()
    {
        canmove = true;
    }

    /// <summary>
    /// Stops the movement of the chunks.
    /// </summary>
    private void StopMovement()
    {
        canmove = false;
    }

    /// <summary>
    /// Gets the size of the given chunk.
    /// </summary>
    /// <param name="_chunk">The chunk of which the size will be retuned.</param>
    /// <returns>The size of the given chunk.</returns>
    private Vector3 GetSize(GameObject _chunk)
    {
        Vector3 size = _chunk.GetComponent<MeshRenderer>().bounds.extents * 2f;
        return size;
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
        if (chunkClones.Count < 1)
        {
            Debug.Log("No chunks to sort.");
            return;
        }

        Vector3 previousChunkPos = chunkClones[0].transform.position;
        for (int i = 0; i < chunkClones.Count; i++)
        {
            if(i > 0)
                previousChunkPos.z += (GetSize(chunkClones[i]).z / 2f);
            chunkClones[i].transform.position = new Vector3(transform.position.x, 0f/*GetSize(_chunkClones[i]).y / 2f*/, previousChunkPos.z);
            previousChunkPos.z += (GetSize(chunkClones[i]).z / 2f);
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
        if (_chunk.transform.position.z < transform.position.z)
            return true;
        else
            return false;
    }
}
