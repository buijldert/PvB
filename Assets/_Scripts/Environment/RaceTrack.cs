﻿using System.Collections;
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
        private float _speed = 1f;

        private bool _canMove;

        private void OnEnable()
        {
            CollisionHandler.OnDeadlyCollision += StopTrack;
            RestartGameButton.OnRestartGame += StartMovement;
        }

        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= StopTrack;
            RestartGameButton.OnRestartGame -= StartMovement;
        }

        private void Start()
        {
            _trackRenderer = GetComponent<MeshRenderer>();
        }
        
        private void Update()
        {
            if(_canMove)
            {
                ScrollTexture();
            }
        }

        /// <summary>
        /// Starts the scrolling movement of the scrolling texture.
        /// </summary>
        private void StartMovement()
        {
            _canMove = true;
        }

        /// <summary>
        /// Scrolls the texture over the quad its attached to.
        /// </summary>
        private void ScrollTexture()
        {
            _textureOffset = new Vector2(0, Time.time * _speed);

            _trackRenderer.material.mainTextureOffset = _textureOffset;
        }

        private void StopTrack()
        {
            _canMove = false;
        }
    }
}