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

        private void OnApplicationQuit()
        {
            isApplicationQuitting = true;
        }

        private void OnDisable()
        {
            if (!isApplicationQuitting && poolDelayCoroutine != null)
                StopCoroutine(poolDelayCoroutine);
                
        }
    }
}