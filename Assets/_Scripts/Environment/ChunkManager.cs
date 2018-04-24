using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _chunkPrefabs;
    [SerializeField] private List<GameObject> _chunkClones = new List<GameObject>();

	private void Start ()
    {
        for (int i = 0; i < 7; i++)
        {
            _chunkClones.Add(GetRandomChunk(Vector3.zero));
        }

        for (int j = 0; j < _chunkClones.Count; j++)
        {
            _chunkClones[j].transform.position = new Vector3(0, 0, 300f + GetSize(_chunkClones[j]).z);
        }

        Vector3 previousChunkPos = _chunkClones[0].transform.position;

        for (int k = 0; k < _chunkClones.Count; k++)
        {
            previousChunkPos.z += (GetSize(_chunkClones[k]).z/2f);
            _chunkClones[k].transform.position = new Vector3(transform.position.x, GetSize(_chunkClones[k]).y / 2f, previousChunkPos.z);
            previousChunkPos.z += (GetSize(_chunkClones[k]).z/2f);
        }
	}

    private Vector3 GetSize(GameObject chunk)
    {
        Vector3 size = chunk.GetComponent<BoxCollider>().size;
        Vector3 scale = chunk.transform.lossyScale;
        Vector3 updatedSize = new Vector3(size.x * scale.x, size.y * scale.y, size.z * scale.z);
        return updatedSize;
    }

    private float GetSizeZ(GameObject chunk)
    {
        float z = chunk.GetComponent<BoxCollider>().size.z * chunk.transform.lossyScale.z;
        return z;
    }

    private GameObject GetRandomChunk(Vector3 _position)
    {
        return SpawnChunk(_chunkPrefabs[Random.Range(0, _chunkPrefabs.Length)], _position);
    }

    private GameObject SpawnChunk(GameObject chunk, Vector3 position)
    {
        return Instantiate(chunk, position, Quaternion.identity);
    }

    private void SortChunks()
    {
        if (_chunkClones.Count < 1)
        {
            Debug.Log("No chunks to sort.");
            return;
        }
        var firstChunkPos = _chunkClones[0].transform.position;
        for (int i = 0; i < _chunkClones.Count; i++)
        {
            _chunkClones[i].transform.position = new Vector3(0, GetSize(_chunkClones[i]).y/2f, firstChunkPos.z + (GetSize(_chunkClones[i]).z * i));
        }
    }
}
