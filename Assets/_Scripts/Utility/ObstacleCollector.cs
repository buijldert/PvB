using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class ObstacleCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ObjectPool.Instance.PoolObject(other.gameObject);
    }
}
