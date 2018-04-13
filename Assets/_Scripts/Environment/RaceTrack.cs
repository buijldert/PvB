using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Utility;

namespace Environment
{
    [RequireComponent(typeof(MeshRenderer))]
    public class RaceTrack : MonoBehaviour
    {
        private MeshRenderer _trackRenderer;

        private Vector2 _textureOffset;
        private float _speed = 0.5f;

        private bool _canMove;

        private void OnEnable()
        {
            CollisionHandler.OnCollision += StopTrack;
            RestartGameButton.OnRestartGame += StartMovement;
        }

        private void OnDisable()
        {
            CollisionHandler.OnCollision -= StopTrack;
            RestartGameButton.OnRestartGame -= StartMovement;
        }

        private void Start()
        {
            _trackRenderer = GetComponent<MeshRenderer>();
        }

        private void StartMovement()
        {
            _canMove = true;
        }
        
        private void Update()
        {
            if(_canMove)
            {
                _textureOffset = new Vector2(0, Time.time * _speed);

                _trackRenderer.material.mainTextureOffset = _textureOffset;
            }
        }

        private void StopTrack()
        {
            _canMove = false;
        }
    }
}