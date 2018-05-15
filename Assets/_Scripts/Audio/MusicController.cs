﻿using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Utility;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicController : MonoBehaviour
    {
        private AudioSource audioSource;

        private void OnEnable()
        {
            CollisionHandler.OnDeadlyCollision += StopMusic;
            RestartGameButton.OnRestartGame += StartMusic;
        }

        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= StopMusic;
            RestartGameButton.OnRestartGame -= StartMusic;
        }

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void StartMusic()
        {
            audioSource.Play();
        }

        private void StopMusic()
        {
            audioSource.Stop();
        }
    }
}