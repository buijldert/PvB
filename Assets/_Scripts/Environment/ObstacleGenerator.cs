using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    public class ObstacleGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _obstaclePrefab;

        private float _spawnDelay = .75f;

        private Coroutine _spawningCoroutine;

        private void Start()
        {
            _spawningCoroutine = StartCoroutine(StartSpawning());
        }

        private IEnumerator StartSpawning()
        {
            _spawnDelay = Random.Range(0.5f, 0.75f);
            yield return new WaitForSeconds(_spawnDelay);
            GameObject obstacleClone = Instantiate(_obstaclePrefab);
            obstacleClone.transform.position = new Vector3(Random.Range(-20f, 20f), transform.position.y, 350f);
            obstacleClone.transform.SetParent(transform);
            _spawningCoroutine = StartCoroutine(StartSpawning());
        }
    }
}