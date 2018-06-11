using System;
using System.Collections;
using UnityEngine;

namespace RR.Components
{
    /// <summary>
    /// This class is responsible for pooling the gameobject it is attached to and removing it from the active pool.
    /// </summary>
    public class PoolOverTime : MonoBehaviour
    {
        public static Action<GameObject> OnObstacleCollection;

        [SerializeField] private float poolTime;

        private Coroutine poolCoroutine;

        /// <summary>
        /// OnEnable() is called before Start() and after Awake().
        /// </summary>
        private void OnEnable()
        {
            if (poolCoroutine != null)
                StopCoroutine(poolCoroutine);

            poolCoroutine = StartCoroutine(PoolDelay());
        }

        /// <summary>
        /// Pools the gameobject after a delay;
        /// </summary>
        /// <returns></returns>
        private IEnumerator PoolDelay()
        {
            yield return new WaitForSeconds(poolTime);
            if (OnObstacleCollection != null)
            {
                OnObstacleCollection(gameObject);
            }
            ObjectPool.instance.PoolObject(gameObject);
        }
    }
}