using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(Rigidbody))]
    public class Obstacle : MonoBehaviour
    {
        private Rigidbody _rb;
        private float _movementSpeed = 200f;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }
        
        private void FixedUpdate()
        {
            _rb.velocity = Vector3.back * _movementSpeed;
        }
    }
}