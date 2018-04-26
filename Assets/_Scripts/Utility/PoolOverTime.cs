using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class PoolOverTime : MonoBehaviour
    {
        [SerializeField] private float _poolTime;
        private Coroutine _poolDelayCoroutine;

        private bool _isApplicationQuitting;

        private void OnEnable()
        {
            _poolDelayCoroutine = StartCoroutine(PoolDelay());
        }

        /// <summary>
        /// Pools the gameObject after the given time.
        /// </summary>
        private IEnumerator PoolDelay()
        {
            yield return new WaitForSeconds(_poolTime);
            ObjectPool.Instance.PoolObject(gameObject);
        }

        private void OnApplicationQuit()
        {
            _isApplicationQuitting = true;
        }

        private void OnDisable()
        {
            if (!_isApplicationQuitting && _poolDelayCoroutine != null)
                StopCoroutine(_poolDelayCoroutine);
                
        }
    }
}