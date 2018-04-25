using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class PoolOverTime : MonoBehaviour
    {
        [SerializeField] private float _poolTime;

        private void OnEnable()
        {
            StartCoroutine(PoolDelay());
        }

        /// <summary>
        /// Pools the gameObject after the given time.
        /// </summary>
        private IEnumerator PoolDelay()
        {
            yield return new WaitForSeconds(_poolTime);
            ObjectPool.Instance.PoolObject(gameObject);
        }
    }
}