using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody _rb;
        private float _movementSpeed = 1000f;
        private float _movementGate = 18f;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            float x = Input.GetAxis("Horizontal");
            _rb.velocity = new Vector3(x * _movementSpeed * Time.deltaTime, _rb.velocity.y, _rb.velocity.z);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -_movementGate, _movementGate), transform.position.y, transform.position.z);
        }
    }
}