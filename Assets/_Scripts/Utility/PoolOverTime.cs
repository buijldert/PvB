using System;
using System.Collections;
using UnityEngine;

namespace Utility
{
    public class PoolOverTime : MonoBehaviour
    {
        public static Action<GameObject> OnObstacleCollection;

        [SerializeField] private float poolTime;

        private Coroutine poolCoroutine;

        private void OnEnable()
        {
            if (poolCoroutine != null)
                StopCoroutine(poolCoroutine);

            poolCoroutine = StartCoroutine(PoolDelay());
        }

        private IEnumerator PoolDelay()
        {
            yield return new WaitForSeconds(poolTime);
            if (OnObstacleCollection != null)
            {
                OnObstacleCollection(gameObject);
            }
            ObjectPool.Instance.PoolObject(gameObject);
        }
    }
}