using System.Collections;
using UnityEngine;

namespace Utility
{
    public class DestroyOverTime : MonoBehaviour
    {
        [SerializeField] private float destroyTime;

        private void OnEnable()
        {
            StartCoroutine(DestroyDelay());
        }

        /// <summary>
        /// Destroys the gameObject after the given time.
        /// </summary>
        private IEnumerator DestroyDelay()
        {
            yield return new WaitForSeconds(destroyTime);
            Destroy(gameObject);
        }
    }
}