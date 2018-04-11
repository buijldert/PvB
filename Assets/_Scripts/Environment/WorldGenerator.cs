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

        private void Start()
        {
            GenerateWorld();
        }

        private void GenerateWorld()
        {
            for (int i = 0; i < _level.Length; i++)
            {
                if(_level[i] > 0)
                {
                    int obstacleChosen = _level[i] - 1;
                    int oppositeObstacle = 0;
                    GameObject obstacleClone = Instantiate(_obstaclePrefabs[obstacleChosen]);
                    if (obstacleChosen == 0)
                        oppositeObstacle = 1;

                    GameObject mirroredObstacle = Instantiate(_obstaclePrefabs[oppositeObstacle]);
                    mirroredObstacle.transform.position = new Vector3(obstacleClone.transform.position.x-25f, obstacleClone.transform.position.y, i * 100f);
                    obstacleClone.transform.position = new Vector3(obstacleClone.transform.position.x, obstacleClone.transform.position.y, i * 100f);
                    obstacleClone.transform.SetParent(transform);
                    mirroredObstacle.transform.SetParent(transform);
                }
            }
        }
    }
}