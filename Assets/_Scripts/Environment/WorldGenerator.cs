using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    public class WorldGenerator : MonoBehaviour
    {
        //0 = nothing.
        //1 = black obstacle.
        //2 = white obstale.
        private int[] _level = new int[10] {0, 1, 2, 0, 1, 2, 1, 0, 1, 2 };

        [SerializeField]private GameObject[] _obstaclePrefabs;

        private float _spawnDelay = 1f;

        private void Start()
        {
            StartCoroutine(GenerateWorld());
        }

        private IEnumerator GenerateWorld()
        {
            for (int i = 0; i < _level.Length; i++)
            {
                if(_level[i] > 0)
                {
                    int obstacleChosen = _level[i] - 1;
                    GameObject obstacleClone = Instantiate(_obstaclePrefabs[obstacleChosen]);
                    GameObject mirroredObstacle = Instantiate(_obstaclePrefabs[obstacleChosen]);
                    mirroredObstacle.transform.position = new Vector3(obstacleClone.transform.position.x-25f, obstacleClone.transform.position.y, 350f);
                    obstacleClone.transform.position = new Vector3(obstacleClone.transform.position.x, obstacleClone.transform.position.y, 350f);
                    obstacleClone.transform.SetParent(transform);
                    mirroredObstacle.transform.SetParent(transform);
                }
                yield return new WaitForSeconds(_spawnDelay);
            }

            StartCoroutine(GenerateWorld());
        }
    }
}