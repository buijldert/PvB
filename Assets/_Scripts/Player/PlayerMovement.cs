using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Utility;

namespace Player
{
    [RequireComponent(typeof(MeshRenderer))]
    public class PlayerMovement : MonoBehaviour
    {
        private MeshRenderer _mr;

        [SerializeField] private Vector3 _leftPos, _rightPos;

        private bool _canMove;

        private void OnEnable()
        {
            CollisionHandler.OnCollision += ResetPlayer;
            RestartGameButton.OnRestartGame += StartMovment;
        }

        private void OnDisable()
        {
            CollisionHandler.OnCollision -= ResetPlayer;
            RestartGameButton.OnRestartGame -= StartMovment;
        }

        private void Start()
        {
            _mr = GetComponent<MeshRenderer>();
            
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _canMove)
            {
                if (gameObject.layer == 11)
                {
                    TransformPlayer(10, _leftPos, Color.white);
                }
                else
                {
                    TransformPlayer(11, _rightPos, Color.black);
                }
            }
        }

        private void StartMovment()
        {
            _canMove = true;
        }

        private void TransformPlayer(int layer, Vector3 positionToMove, Color colorToMake)
        {
            gameObject.layer = layer;
            transform.position = positionToMove;
            _mr.material.DOColor(colorToMake, 0f);
        }

        private void ResetPlayer()
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            _canMove = false;
            TransformPlayer(11, _rightPos, Color.black);
        }
    }
}