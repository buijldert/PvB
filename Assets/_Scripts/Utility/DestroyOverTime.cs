using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class DestroyOverTime : MonoBehaviour
    {
        [SerializeField] private float _destroyTime;

        private void OnEnable()
        {
            StartCoroutine(DestroyDelay());
        }

        /// <summary>
        /// Destroys the gameObject after the given time.
        /// </summary>
        private IEnumerator DestroyDelay()
        {
            yield return new WaitForSeconds(_destroyTime);
            Destroy(gameObject);
        }
    }
}