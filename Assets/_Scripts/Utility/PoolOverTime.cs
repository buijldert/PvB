using System.Collections;
using UnityEngine;

namespace Utility
{
    public class PoolOverTime : MonoBehaviour
    {
        [SerializeField] private float poolTime;
        private Coroutine poolDelayCoroutine;

        private bool isApplicationQuitting;

        private void OnEnable()
        {
            if (poolDelayCoroutine != null)
                StopCoroutine(poolDelayCoroutine);
            poolDelayCoroutine = StartCoroutine(PoolDelay());
        }

        /// <summary>
        /// Pools the gameObject after the given time.
        /// </summary>
        private IEnumerator PoolDelay()
        {
            yield return new WaitForSeconds(poolTime);
            ObjectPool.Instance.PoolObject(gameObject);
        }
    }
}