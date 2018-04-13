using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    [RequireComponent(typeof(Collider))]
    public class CollisionHandler : MonoBehaviour
    {
        public static Action OnCollision;

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Obstacle")
            {
                if (OnCollision != null)
                    OnCollision();
            }
        }
    }
}